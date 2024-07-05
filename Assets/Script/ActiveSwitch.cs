using UnityEngine;

public class ActiveSwitch : MonoBehaviour
{
    public GameObject observedObject; // �Ď��Ώۂ̃I�u�W�F�N�g
    public GameObject[] linkedObjects; // �؂�ւ���Ώۂ̃I�u�W�F�N�g�̔z��

    private bool previousState; // �Ď��Ώۂ̃I�u�W�F�N�g�̑O��̃A�N�e�B�u���

    void Start()
    {
        if (observedObject == null)
        {
            Debug.LogError("Observed object is not assigned!");
            enabled = false; // �X�N���v�g�𖳌������ăG���[��h��
            return;
        }

        // ������Ԃ�ۑ�
        previousState = observedObject.activeSelf;
    }

    void Update()
    {
        if (observedObject == null) return;

        // �Ď��Ώۂ̃I�u�W�F�N�g�̃A�N�e�B�u��Ԃ��ω����������`�F�b�N
        if (observedObject.activeSelf != previousState)
        {
            ToggleLinkedObjects();
            previousState = observedObject.activeSelf; // �O��̏�Ԃ��X�V
        }
    }

    private void ToggleLinkedObjects()
    {
        bool newState = observedObject.activeSelf;

        // �����N���ꂽ�I�u�W�F�N�g�̃A�N�e�B�u��Ԃ�؂�ւ���
        foreach (var obj in linkedObjects)
        {
            if (obj != null)
            {
                obj.SetActive(newState);
            }
        }
    }
}
