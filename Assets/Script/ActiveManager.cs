using UnityEngine;
using System.Collections.Generic;

public class ActiveManager : MonoBehaviour
{
    private List<GaugeController> inactiveObjects = new List<GaugeController>();

    void Update()
    {
        // スペースキーを押したときに非アクティブオブジェクトをアクティブにする
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReactivateObjects();
        }
    }

    public void RegisterInactiveObject(GaugeController obj)
    {
        if (!inactiveObjects.Contains(obj))
        {
            inactiveObjects.Add(obj);
        }
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
