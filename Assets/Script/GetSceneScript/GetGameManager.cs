using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using System;

public class GetGameManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public int numberOfAnimals = 5;
    public Vector2 spawnAreaMin = new Vector2(-5, -5);
    public Vector2 spawnAreaMax = new Vector2(5, 5);

    public ClickZoom clickZoomScript;
    public GaugeMiniGame gaugeMiniGameScript;
    public GameObject backgroundOverlay;
    public Camera mainCamera;

    // TextMeshProの参照を追加
    public TextMeshProUGUI successText;
    public TextMeshProUGUI failureText;

    private List<Animal> capturedAnimals = new List<Animal>();
    private Animal currentAnimal;

    private void Start()
    {
        SpawnAnimals();
    }

    private void Update()
    {
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
                    backgroundOverlay.GetComponent<SpriteRenderer>().sortingOrder = 90;
                    animal.GetComponent<SpriteRenderer>().sortingOrder = 100;
                }
            }
        }
    }

    public void OnZoomComplete()
    {
        gaugeMiniGameScript.StartMiniGame(OnMiniGameComplete);
    }

    private void OnMiniGameComplete(bool isSuccess)
    {
        // 成功または失敗時の動作を1秒待ってから行う
        StartCoroutine(HandleMiniGameResult(isSuccess));
    }

    private IEnumerator HandleMiniGameResult(bool isSuccess)
    {
        if (isSuccess && currentAnimal != null)
        {
            ShowSuccessText(); // 成功時のテキストを表示
        }
        else
        {
            ShowFailureText(); // 失敗時のテキストを表示
            if (currentAnimal != null)
            {
                currentAnimal.StartMoving();  // 失敗時は動物を再び動かす
            }
        }

        // 1秒待つ
        yield return new WaitForSeconds(1f);

        // テキストを非表示にする
        HideSuccessText();
        HideFailureText();

        // 成功時に動物を捕獲する
        if (isSuccess && currentAnimal != null)
        {
            CaptureAnimal(currentAnimal);
        }

        // カメラのサイズを元に戻す
        mainCamera.orthographicSize = 5f;

        // ズームをリセットする
        clickZoomScript.ResetZoom();

        // 現在の動物をクリアする
        currentAnimal = null;
    }

    private void CaptureAnimal(Animal animal)
    {
        capturedAnimals.Add(animal);
        UnityEngine.Debug.Log($"Captured Animal: {animal.type}");
        Destroy(animal.gameObject);
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

    private void SpawnAnimals()
    {
        for (int i = 0; i < numberOfAnimals; i++)
        {
            int prefabIndex = UnityEngine.Random.Range(0, animalPrefabs.Length);
            GameObject selectedPrefab = animalPrefabs[prefabIndex];

            Vector3 spawnPosition = new Vector3(
                UnityEngine.Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                UnityEngine.Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0f
            );

            GameObject animalObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            Animal animal = animalObject.GetComponent<Animal>();

            animal.type = (AnimalType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(AnimalType)).Length);
            animal.StartMoving();
        }
    }

    private void ShowSuccessText()
    {
        successText.gameObject.SetActive(true);
    }

    private void ShowFailureText()
    {
        failureText.gameObject.SetActive(true);
    }

    private void HideSuccessText()
    {
        successText.gameObject.SetActive(false);
    }

    private void HideFailureText()
    {
        failureText.gameObject.SetActive(false);
    }
}
