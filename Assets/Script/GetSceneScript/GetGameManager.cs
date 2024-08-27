using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GetGameManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;  // 動物のプレハブを格納する配列
    public int numberOfAnimals = 5;     // 生成する動物の数
    public Vector2 spawnAreaMin = new Vector2(-5, -5);  // スポーンエリアの最小値
    public Vector2 spawnAreaMax = new Vector2(5, 5);    // スポーンエリアの最大値

    private List<Animal> capturedAnimals = new List<Animal>();  // 捕獲した動物を保持するリスト

    private void Start()
    {
        SpawnAnimals();  // ゲーム開始時に動物を生成
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
                    // ミニゲームを始める代わりに、動物を直接捕獲
                    CaptureAnimal(animal);
                }
            }
        }
    }

    private void CaptureAnimal(Animal animal)
    {
        capturedAnimals.Add(animal);
        UnityEngine.Debug.Log($"Captured Animal: {animal.type}");
        Destroy(animal.gameObject);  // 動物を削除
        SaveCapturedAnimals();       // 捕獲した動物を保存
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
