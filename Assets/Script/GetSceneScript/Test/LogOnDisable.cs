using UnityEngine;

public class LogOnDisable : MonoBehaviour
{
    void OnDisable()
    {
        // ���̃X�N���v�g���A�^�b�`���ꂽ�I�u�W�F�N�g����A�N�e�B�u�ɂȂ������ɌĂ΂��
        Debug.Log($"{gameObject.name} �I�u�W�F�N�g����A�N�e�B�u�ɂȂ�܂��� (�X�N���v�g: {this.GetType().Name})");
    }
}
