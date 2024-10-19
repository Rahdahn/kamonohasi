using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // �����̎�ނ��Ƃ̃J�E���g��\������TextMeshPro
    public TextMeshProUGUI d1CountText;
    public TextMeshProUGUI d2CountText;
    public TextMeshProUGUI d3CountText;
    public TextMeshProUGUI d4CountText;
    public TextMeshProUGUI d5CountText;

    private List<Animal> capturedAnimals = new List<Animal>();
    private Animal currentAnimal;
    private int originalAnimalSortingOrder; // �����̌���Order in Layer��ێ�����ϐ�
    private float timeRemaining = 60f;      // �^�C�}�[�̏����l�i60�b�j
    private bool timerIsRunning = true;     // �^�C�}�[�����쒆���ǂ����̃t���O
    private bool isClickable = true;         // �N���b�N�\���ǂ����̃t���O

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

        // �ߊl���������̃J�E���g��������
        UpdateAnimalCountDisplay();
    }

    private void Update()
    {
        if (timerIsRunning && isClickable) // �N���b�N�\�ȏꍇ�̂ݏ���
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

        // �Q�[�W�~�j�Q�[�����~�iDisable�ɂ���j
        if (gaugeMiniGameScript != null)
        {
            gaugeMiniGameScript.gameObject.SetActive(false); // �X�N���v�g��GameObject�𖳌���
        }

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
    }


    public void OnZoomComplete()
    {
        isClickable = false; // �~�j�Q�[�����̓N���b�N�𖳌���
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

        isClickable = true; // �N���b�N���ēx�L����
        yield return new WaitForSeconds(5f);
        SpawnAnimals(1); // �V����������1�̃X�|�[��
    }

    private void CaptureAnimal(Animal animal)
    {
        capturedAnimals.Add(animal);
        UnityEngine.Debug.Log($"Captured Animal: {animal.type}");
        Destroy(animal.gameObject);
        spawnedAnimals.Remove(animal.gameObject); // ���X�g����폜

        // �ߊl���������̎�ނ��Ƃ̃J�E���g���X�V
        switch (animal.type)
        {
            case AnimalType.D1:
                GameData.D1Count++;
                break;
            case AnimalType.D2:
                GameData.D2Count++;
                break;
            case AnimalType.D3:
                GameData.D3Count++;
                break;
            case AnimalType.D4:
                GameData.D4Count++;
                break;
            case AnimalType.D5:
                GameData.D5Count++;
                break;
        }
        UpdateAnimalCountDisplay();
    }

    private void UpdateAnimalCountDisplay()
    {
        // �e��ނ̃J�E���g��TextMeshPro�ɔ��f
        d1CountText.text = $"X {GameData.D1Count}";
        d2CountText.text = $"X {GameData.D2Count}";
        d3CountText.text = $"X {GameData.D3Count}";
        d4CountText.text = $"X {GameData.D4Count}";
        d5CountText.text = $"X {GameData.D5Count}";
    }

    private void SpawnAnimals(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int prefabIndex = UnityEngine.Random.Range(0, animalPrefabs.Length);
            GameObject prefab = animalPrefabs[prefabIndex];
            Vector2 spawnPos = new Vector2(
                UnityEngine.Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                UnityEngine.Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            GameObject animal = Instantiate(prefab, spawnPos, Quaternion.identity);
            spawnedAnimals.Add(animal); // �X�|�[���������������X�g�ɒǉ�
        }
    }
}