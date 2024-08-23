using UnityEngine;
using System.Collections.Generic;

public class ActiveManager : MonoBehaviour
{
    private List<GaugeController> inactiveObjects = new List<GaugeController>();

    // ���̃X�N���v�g���A�^�b�`�����I�u�W�F�N�g��BoxCollider2D���K�v�ł�

    void Start()
    {
        // BoxCollider2D���A�^�b�`����Ă��邩�m�F
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
        // �}�E�X�����̃I�u�W�F�N�g���N���b�N�����Ƃ��ɔ�A�N�e�B�u�I�u�W�F�N�g���A�N�e�B�u�ɂ���
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
