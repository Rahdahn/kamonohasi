using UnityEngine;

public class Hdeanimal : MonoBehaviour
{
    public GameObject targetObject; // �ΏۂƂȂ�I�u�W�F�N�g
    public string scriptTypeName; // �����ɂ���X�N���v�g�̌^��

    private MonoBehaviour scriptToDisable; // �����ɂ���X�N���v�g

    private void Start()
    {
        // �w�肳�ꂽ�^���̃R���|�[�l���g������
        scriptToDisable = targetObject.GetComponent(scriptTypeName) as MonoBehaviour;

        if (scriptToDisable == null)
        {
            Debug.LogError("Script not found on target object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            // �A�^�b�`���ꂽ�X�N���v�g�𖳌��ɂ���
            if (scriptToDisable != null)
            {
                scriptToDisable.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            // �A�^�b�`���ꂽ�X�N���v�g���ēx�L���ɂ���
            if (scriptToDisable != null)
            {
                scriptToDisable.enabled = true;
            }
        }
    }
}
