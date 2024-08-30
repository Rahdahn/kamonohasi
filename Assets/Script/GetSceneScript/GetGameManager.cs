using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro用

public class GetGameManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public int numberOfAnimals = 5;
    public Vector2 spawnAreaMin = new Vector2(-5, -5);
    public Vector2 spawnAreaMax = new Vector2(5, 5);

    public ClickZoom clickZoomScript;
    public GaugeMiniGame gaugeMiniGameScript;
    public GameObject backgroundOverlay;
    public TextMeshProUGUI successText; // 捕獲成功のテキスト
    public TextMeshProUGUI failText;    // 捕獲失敗のテキスト
    public TextMeshProUGUI timerText;   // タイマー表示用のTextMeshPro
    public Canvas gameOverCanvas;       // 時間切れ時に表示するCanvas

    private List<Animal> capturedAnimals = new List<Animal>();
    private Animal currentAnimal;
    private int originalAnimalSortingOrder; // 動物の元のOrder in Layerを保持する変数
    private float timeRemaining = 60f;      // タイマーの初期値（60秒）
    private bool timerIsRunning = true;     // タイマーが動作中かどうかのフラグ

    private List<GameObject> spawnedAnimals = new List<GameObject>(); // スポーンした動物を追跡するリスト

    private void Start()
    {
        SpawnAnimals(numberOfAnimals); // 初期スポーンの数に変更
        if (backgroundOverlay != null)
        {
            backgroundOverlay.SetActive(false); // 初期状態でオーバーレイを非表示
        }
        if (successText != null)
        {
            successText.gameObject.SetActive(false); // 初期状態で成功テキストを非表示
        }
        if (failText != null)
        {
            failText.gameObject.SetActive(false); // 初期状態で失敗テキストを非表示
        }
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(false); // 初期状態でCanvasを非表示
        }
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimeExpired();
            }
        }

        if (Input.GetMouseButtonDown(0) && currentAnimal == null)
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

            if (hit.collider != null)
            {
                Animal animal = hit.collider.GetComponent<Animal>();
                if (animal != null)
                {
                    currentAnimal = animal;
                    currentAnimal.StopMoving();  // 動物の動きを止める
                    clickZoomScript.StartZoom(animal.transform.position);

                    // オーバーレイと動物のOrder in Layerを調整
                    var spriteRenderer = backgroundOverlay.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sortingOrder = 90;
                    }
                    var animalRenderer = animal.GetComponent<SpriteRenderer>();
                    if (animalRenderer != null)
                    {
                        originalAnimalSortingOrder = animalRenderer.sortingOrder; // 元のOrder in Layerを保存
                        animalRenderer.sortingOrder = 100;
                    }

                    if (backgroundOverlay != null)
                    {
                        backgroundOverlay.SetActive(true); // オーバーレイを表示
                    }
                }
            }
        }
    }

    private void UpdateTimerDisplay(float time)
    {
        // 残り時間を分と秒に分けて表示
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnTimeExpired()
    {
        // タイマーが0になった時の処理
        UnityEngine.Debug.Log("Time's up!");

        // タイマーを非表示
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

        // ゲームオーバーCanvasを表示
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(true);
        }

        // 全ての動物を消去
        foreach (var animal in spawnedAnimals)
        {
            if (animal != null)
            {
                Destroy(animal);
            }
        }
        spawnedAnimals.Clear();

        // ゲーム終了時の追加処理があればここに記述
    }

    public void OnZoomComplete()
    {
        gaugeMiniGameScript.StartMiniGame(OnMiniGameComplete);
    }

    private void OnMiniGameComplete(bool isSuccess)
    {
        StartCoroutine(HandleMiniGameResult(isSuccess));
    }

    private IEnumerator HandleMiniGameResult(bool isSuccess)
    {
        if (isSuccess)
        {
            if (successText != null)
            {
                successText.gameObject.SetActive(true); // 成功テキストを表示
            }
            if (currentAnimal != null)
            {
                CaptureAnimal(currentAnimal);
            }
        }
        else
        {
            if (failText != null)
            {
                failText.gameObject.SetActive(true); // 失敗テキストを表示
            }
            // 動物が動き出すように設定し、Order in Layerを元に戻す
            if (currentAnimal != null)
            {
                var animalRenderer = currentAnimal.GetComponent<SpriteRenderer>();
                if (animalRenderer != null)
                {
                    animalRenderer.sortingOrder = originalAnimalSortingOrder; // 元のOrder in Layerに戻す
                }
                currentAnimal.StartMoving();
            }
        }

        yield return new WaitForSeconds(1f); // 1秒間待つ

        // オーバーレイとテキストを非表示にする
        if (backgroundOverlay != null)
        {
            backgroundOverlay.SetActive(false); // オーバーレイを非表示
        }
        if (successText != null)
        {
            successText.gameObject.SetActive(false); // 成功テキストを非表示
        }
        if (failText != null)
        {
            failText.gameObject.SetActive(false); // 失敗テキストを非表示
        }

        clickZoomScript.ResetZoom();
        currentAnimal = null;

        // 新しい動物をスポーンする前に5秒待つ
        yield return new WaitForSeconds(5f);
        SpawnAnimals(1); // 新しい動物を1体スポーン
    }

    private void CaptureAnimal(Animal animal)
    {
        capturedAnimals.Add(animal);
        UnityEngine.Debug.Log($"Captured Animal: {animal.type}");
        Destroy(animal.gameObject);
        spawnedAnimals.Remove(animal.gameObject); // リストから削除
        SaveCapturedAnimals();
    }

    private void SaveCapturedAnimals()
    {
        PlayerPrefs.SetInt("CapturedCount", capturedAnimals.Count);

        for (int i = 0; i < capturedAnimals.Count; i++)
        {
            PlayerPrefs.SetInt("CapturedAnimal_" + i, (int)capturedAnimals[i].type);
        }

        PlayerPrefs.Save();
    }

    private void SpawnAnimals(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int prefabIndex = UnityEngine.Random.Range(0, animalPrefabs.Length);
            GameObject selectedPrefab = animalPrefabs[prefabIndex];

            Vector3 spawnPosition = new Vector3(
                UnityEngine.Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                UnityEngine.Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                100f // Z座標を設定
            );

            GameObject animalObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            Animal animal = animalObject.GetComponent<Animal>();

            animal.type = (AnimalType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(AnimalType)).Length);
            animal.StartMoving();
            spawnedAnimals.Add(animalObject); // スポーンした動物をリストに追加
        }
    }
}
