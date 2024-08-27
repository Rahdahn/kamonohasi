using System.Collections.Generic;
using UnityEngine;

public class GetGameManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public int numberOfAnimals = 5;
    public Vector2 spawnAreaMin = new Vector2(-5, -5);
    public Vector2 spawnAreaMax = new Vector2(5, 5);
    public Camera mainCamera; // ƒJƒƒ‰‚Ö‚ÌQÆ‚ğ’Ç‰Á
    public ClickZoom clickZoomScript;
    public GaugeMiniGame gaugeMiniGameScript;

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
                    currentAnimal.StopMoving();  // “®•¨‚Ì“®‚«‚ğ~‚ß‚é
                    clickZoomScript.StartZoom(animal.transform.position);
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
        if (isSuccess && currentAnimal != null)
        {
            CaptureAnimal(currentAnimal);
            clickZoomScript.ResetZoom();
        }
        else if (currentAnimal != null)
        {
            // Šl“¾‚É¸”s‚µ‚½ê‡‚É“®•¨‚Ì“®‚«‚ğÄŠJ‚·‚é
            currentAnimal.StartMoving();
            clickZoomScript.ResetZoom();
        }
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
                100f // ZÀ•W‚ğİ’è
            );

            GameObject animalObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            Animal animal = animalObject.GetComponent<Animal>();

            animal.type = (AnimalType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(AnimalType)).Length);
            animal.StartMoving();
        }
    }
}
