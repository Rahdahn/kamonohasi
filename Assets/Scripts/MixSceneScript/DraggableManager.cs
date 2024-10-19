using System.Collections.Generic;
using UnityEngine;

public class DraggableManager : MonoBehaviour
{
    // Prefab��Hierarchy��̃I�u�W�F�N�g���`
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

    // �h���b�O�\�ȃI�u�W�F�N�g�̃X�i�b�v�ʒu���Ǘ����邽�߂̎���
    private Dictionary<Draggable, Dictionary<MonoBehaviour, Vector2>> draggableSnapPositions;

    // ���������\�b�h
    private void Start()
    {
        InitializeDraggables();
    }

    // �h���b�O�\�ȃI�u�W�F�N�g�����������郁�\�b�h
    private void InitializeDraggables()
    {
        draggableSnapPositions = new Dictionary<Draggable, Dictionary<MonoBehaviour, Vector2>>();

        // �K�v�Ȉ����𐳂����w�肷��
        CreateAndAttachAnimals(d1Prefab, d1Object, GameData.D1Count);
        CreateAndAttachAnimals(d2Prefab, d2Object, GameData.D2Count);
        CreateAndAttachAnimals(d3Prefab, d3Object, GameData.D3Count);
        CreateAndAttachAnimals(d4Prefab, d4Object, GameData.D4Count);
        CreateAndAttachAnimals(d5Prefab, d5Object, GameData.D5Count);
    }

    // Prefab���瓮���𐶐����A�w��̃I�u�W�F�N�g�ɃA�^�b�`���郁�\�b�h
    public void CreateAndAttachAnimals(GameObject prefab, GameObject referenceObject, int count)
    {
        if (prefab == null || referenceObject == null || count <= 0)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject newAnimal = Instantiate(prefab);
            newAnimal.transform.SetParent(referenceObject.transform.parent);
            newAnimal.transform.position = referenceObject.transform.position;
            newAnimal.transform.localScale = referenceObject.transform.localScale;
            newAnimal.transform.rotation = referenceObject.transform.rotation;

            Draggable draggableComponent = newAnimal.GetComponent<Draggable>();
            if (draggableComponent != null)
            {
                draggableSnapPositions[draggableComponent] = new Dictionary<MonoBehaviour, Vector2>();
            }
        }
    }
}
