using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GetGameManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;  // �����̃v���n�u���i�[����z��
    public int numberOfAnimals = 5;     // �������铮���̐�
    public Vector2 spawnAreaMin = new Vector2(-5, -5);  // �X�|�[���G���A�̍ŏ��l
    public Vector2 spawnAreaMax = new Vector2(5, 5);    // �X�|�[���G���A�̍ő�l

    private List<Animal> capturedAnimals = new List<Animal>();  // �ߊl����������ێ����郊�X�g

    private void Start()
    {
        SpawnAnimals();  // �Q�[���J�n���ɓ����𐶐�
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

            if (hit.collider != null)
            {
                Animal animal = hit.collider.GetComponent<Animal>();
                if (animal != null)
                {
                    // �~�j�Q�[�����n�߂����ɁA�����𒼐ڕߊl
                    CaptureAnimal(animal);
                }
            }
        }
    }

    private void CaptureAnimal(Animal animal)
    {
        capturedAnimals.Add(animal);
        UnityEngine.Debug.Log($"Captured Animal: {animal.type}");
        Destroy(animal.gameObject);  // �������폜
        SaveCapturedAnimals();       // �ߊl����������ۑ�
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
        }
    }
}
