using System.Collections.Generic;
using UnityEngine;

public class CollectSceneManager : MonoBehaviour
{
    public GameObject d1Prefab;
    public GameObject d2Prefab;
    public GameObject d3Prefab;
    public GameObject d4Prefab;
    public GameObject d5Prefab;

    public GameObject d1Object;
    public GameObject d2Object;
    public GameObject d3Object;
    public GameObject d4Object;
    public GameObject d5Object;

    public List<MonoBehaviour> dropAreas;

    private void Start()
    {
        InitializeDraggables();
    }

    private void InitializeDraggables()
    {
        // CollectibleManager.Instance.GetCollectedCountを使って獲得した動物の数を取得
        CreateAndAttachAnimals(d1Prefab, d1Object, "D1");
        CreateAndAttachAnimals(d2Prefab, d2Object, "D2");
        CreateAndAttachAnimals(d3Prefab, d3Object, "D3");
        CreateAndAttachAnimals(d4Prefab, d4Object, "D4");
        CreateAndAttachAnimals(d5Prefab, d5Object, "D5");
    }

    private void CreateAndAttachAnimals(GameObject prefab, GameObject targetObject, string tag)
    {
        int count = CollectibleManager.Instance.GetCollectedCount(tag);

        for (int i = 0; i < count; i++)
        {
            GameObject animal = Instantiate(prefab, targetObject.transform.position, targetObject.transform.rotation);
            animal.transform.localScale = targetObject.transform.localScale;

            Draggable draggable = animal.GetComponent<Draggable>();
            if (draggable != null)
            {
                // スナップポジションはDropAreaManagerで管理されるため、ここでは設定しない
                draggable.tag = tag;
            }
        }

        targetObject.SetActive(false);
    }
}
