using UnityEngine;

public class OBActivator : MonoBehaviour
{
    public void Deactivate()
    {
        // �����ɃI�u�W�F�N�g���A�N�e�B�u�����鏈����ǉ�
        gameObject.SetActive(false);
        Debug.Log(gameObject.name + " deactivated!");
    }
}
