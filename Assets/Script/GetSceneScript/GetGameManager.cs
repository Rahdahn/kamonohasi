using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro�p

public class GetGameManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public int numberOfAnimals = 5;
    public Vector2 spawnAreaMin = new Vector2(-5, -5);
    public Vector2 spawnAreaMax = new Vector2(5, 5);

    public ClickZoom clickZoomScript;
    public GaugeMiniGame gaugeMiniGameScript;
    public GameObject backgroundOverlay;
    public TextMeshProUGUI successText; // �ߊl�����̃e�L�X�g
    public TextMeshProUGUI failText;    // �ߊl���s�̃e�L�X�g
    public TextMeshProUGUI timerText;   // �^�C�}�[�\���p��TextMeshPro
    public Canvas gameOverCanvas;       // ���Ԑ؂ꎞ�ɕ\������Canvas

    private List<Animal> capturedAnimals = new List<Animal>();
    private Animal currentAnimal;
    private int originalAnimalSortingOrder; // �����̌���Order in Layer��ێ�����ϐ�
    private float timeRemaining = 60f;      // �^�C�}�[�̏����l�i60�b�j
    private bool timerIsRunning = true;     // �^�C�}�[�����쒆���ǂ����̃t���O

    private List<GameObject> spawnedAnimals = new List<GameObject>(); // �X�|�[������������ǐՂ��郊�X�g

    private void Start()
    {
        SpawnAnimals(numberOfAnimals); // �����X�|�[���̐��ɕύX
        if (backgroundOverlay != null)
        {
            backgroundOverlay.SetActive(false); // ������ԂŃI�[�o�[���C���\��
        }
        if (successText != null)
        {
            successText.gameObject.SetActive(false); // ������ԂŐ����e�L�X�g���\��
        }
        if (failText != null)
        {
            failText.gameObject.SetActive(false); // ������ԂŎ��s�e�L�X�g���\��
        }
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(false); // ������Ԃ�Canvas���\��
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
                    currentAnimal.StopMoving();  // �����̓������~�߂�
                    clickZoomScript.StartZoom(animal.transform.position);

                    // �I�[�o�[���C�Ɠ�����Order in Layer�𒲐�
                    var spriteRenderer = backgroundOverlay.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sortingOrder = 90;
                    }
                    var animalRenderer = animal.GetComponent<SpriteRenderer>();
                    if (animalRenderer != null)
                    {
                        originalAnimalSortingOrder = animalRenderer.sortingOrder; // ����Order in Layer��ۑ�
                        animalRenderer.sortingOrder = 100;
                    }

                    if (backgroundOverlay != null)
                    {
                        backgroundOverlay.SetActive(true); // �I�[�o�[���C��\��
                    }
                }
            }
        }
    }

    private void UpdateTimerDisplay(float time)
    {
        // �c�莞�Ԃ𕪂ƕb�ɕ����ĕ\��
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnTimeExpired()
    {
        // �^�C�}�[��0�ɂȂ������̏���
        UnityEngine.Debug.Log("Time's up!");

        // �^�C�}�[���\��
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

        // �Q�[���I�[�o�[Canvas��\��
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(true);
        }

        // �S�Ă̓���������
        foreach (var animal in spawnedAnimals)
        {
            if (animal != null)
            {
                Destroy(animal);
            }
        }
        spawnedAnimals.Clear();

        // �Q�[���I�����̒ǉ�����������΂����ɋL�q
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
                successText.gameObject.SetActive(true); // �����e�L�X�g��\��
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
                failText.gameObject.SetActive(true); // ���s�e�L�X�g��\��
            }
            // �����������o���悤�ɐݒ肵�AOrder in Layer�����ɖ߂�
            if (currentAnimal != null)
            {
                var animalRenderer = currentAnimal.GetComponent<SpriteRenderer>();
                if (animalRenderer != null)
                {
                    animalRenderer.sortingOrder = originalAnimalSortingOrder; // ����Order in Layer�ɖ߂�
                }
                currentAnimal.StartMoving();
            }
        }

        yield return new WaitForSeconds(1f); // 1�b�ԑ҂�

        // �I�[�o�[���C�ƃe�L�X�g���\���ɂ���
        if (backgroundOverlay != null)
        {
            backgroundOverlay.SetActive(false); // �I�[�o�[���C���\��
        }
        if (successText != null)
        {
            successText.gameObject.SetActive(false); // �����e�L�X�g���\��
        }
        if (failText != null)
        {
            failText.gameObject.SetActive(false); // ���s�e�L�X�g���\��
        }

        clickZoomScript.ResetZoom();
        currentAnimal = null;

        // �V�����������X�|�[������O��5�b�҂�
        yield return new WaitForSeconds(5f);
        SpawnAnimals(1); // �V����������1�̃X�|�[��
    }

    private void CaptureAnimal(Animal animal)
    {
        capturedAnimals.Add(animal);
        UnityEngine.Debug.Log($"Captured Animal: {animal.type}");
        Destroy(animal.gameObject);
        spawnedAnimals.Remove(animal.gameObject); // ���X�g����폜
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
                100f // Z���W��ݒ�
            );

            GameObject animalObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            Animal animal = animalObject.GetComponent<Animal>();

            animal.type = (AnimalType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(AnimalType)).Length);
            animal.StartMoving();
            spawnedAnimals.Add(animalObject); // �X�|�[���������������X�g�ɒǉ�
        }
    }
}
