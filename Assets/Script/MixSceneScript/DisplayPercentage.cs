using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayPercentage : MonoBehaviour
{
    // TextMeshPro�R���|�[�l���g�ւ̎Q��
    public TextMeshProUGUI percentageText;

    // 25�̃C���[�W��ێ�����z��
    public UnityEngine.UI.Image[] images;

    void Start()
    {
        // PlayerPrefs����p�[�Z���e�[�W��ǂݍ���
        float percentage = PlayerPrefs.GetFloat("DropOrderPercentage", 0.0f);
        string order = PlayerPrefs.GetString("DropOrder", "");

        // �p�[�Z���e�[�W���e�L�X�g�ɕϊ����ATextMeshPro�R���|�[�l���g�ɐݒ�
        percentageText.text = $"{percentage}��\n�J���m�n�V�I";

        // �C���[�W��\��
        ShowImageByOrder(order);
    }

    void ShowImageByOrder(string order)
    {
        // �S�ẴC���[�W���\���ɂ���
        foreach (var image in images)
        {
            image.gameObject.SetActive(false);
        }

        // ���ԂɊ�Â��đΉ�����C���[�W��\��
        switch (order)
        {
            case "D1,D5,D4,D3": images[0].gameObject.SetActive(true); break;
            case "D1,D5,D3,D4": images[1].gameObject.SetActive(true); break;
            case "D1,D3,D4,D5": images[2].gameObject.SetActive(true); break;
            case "D1,D3,D5,D4": images[3].gameObject.SetActive(true); break;
            case "D1,D4,D5,D3": images[4].gameObject.SetActive(true); break;
            case "D1,D4,D3,D5": images[5].gameObject.SetActive(true); break;
            case "D5,D1,D4,D3": images[6].gameObject.SetActive(true); break;
            case "D5,D1,D3,D4": images[7].gameObject.SetActive(true); break;
            case "D5,D4,D1,D3": images[8].gameObject.SetActive(true); break;
            case "D5,D4,D3,D1": images[9].gameObject.SetActive(true); break;
            case "D5,D3,D1,D4": images[10].gameObject.SetActive(true); break;
            case "D5,D3,D4,D1": images[11].gameObject.SetActive(true); break;
            case "D4,D5,D3,D1": images[12].gameObject.SetActive(true); break;
            case "D4,D5,D1,D3": images[13].gameObject.SetActive(true); break;
            case "D4,D1,D3,D5": images[14].gameObject.SetActive(true); break;
            case "D4,D1,D5,D3": images[15].gameObject.SetActive(true); break;
            case "D4,D3,D1,D5": images[16].gameObject.SetActive(true); break;
            case "D4,D3,D5,D1": images[17].gameObject.SetActive(true); break;
            case "D3,D1,D4,D5": images[18].gameObject.SetActive(true); break;
            case "D3,D1,D5,D4": images[19].gameObject.SetActive(true); break;
            case "D3,D4,D1,D5": images[20].gameObject.SetActive(true); break;
            case "D3,D4,D5,D1": images[21].gameObject.SetActive(true); break;
            case "D3,D5,D4,D1": images[22].gameObject.SetActive(true); break;
            case "D3,D5,D1,D4": images[23].gameObject.SetActive(true); break;
            default: images[24].gameObject.SetActive(true); break;
        }
    }
}
