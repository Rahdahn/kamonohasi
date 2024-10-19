using UnityEngine;

public class DisplayObjectOnSceneLoad : MonoBehaviour
{
    public GameObject objectToShow; // �\���������I�u�W�F�N�g

    private void Start()
    {
        // �Q�[���f�[�^�̍��v�J�E���g���`�F�b�N
        int totalCount = GameData.D1Count + GameData.D2Count + GameData.D3Count + GameData.D4Count + GameData.D5Count;

        // ���v��4�ȉ��̏ꍇ�̓I�u�W�F�N�g��\��
        if (totalCount < 4)
        {
            ShowObject();
        }
        else
        {
            HideObject();
        }
    }

    private void ShowObject()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(true); // �I�u�W�F�N�g��\��
        }
    }

    private void HideObject()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(false); // �I�u�W�F�N�g���\��
        }
    }
}
