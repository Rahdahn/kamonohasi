using UnityEngine;
using System.Collections.Generic;

public class ActiveManager : MonoBehaviour
{
    private List<GaugeController> inactiveObjects = new List<GaugeController>();

    // このスクリプトがアタッチされるオブジェクトにBoxCollider2Dが必要です

    void Start()
    {
        // BoxCollider2Dがアタッチされているか確認
        if (GetComponent<BoxCollider2D>() == null)
        {
            Debug.LogError("This object needs a BoxCollider2D component!");
        }
    }

    public void RegisterInactiveObject(GaugeController obj)
    {
        if (!inactiveObjects.Contains(obj))
        {
            inactiveObjects.Add(obj);
        }
    }

    void OnMouseDown()
    {
        // マウスがこのオブジェクトをクリックしたときに非アクティブオブジェクトをアクティブにする
        ReactivateObjects();
    }

    private void ReactivateObjects()
    {
        foreach (var obj in inactiveObjects)
        {
            if (obj != null)
            {
                obj.Reactivate();
            }
        }
        inactiveObjects.Clear();
    }
}
