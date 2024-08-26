using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro���g�p���邽�߂ɕK�v

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad; // �C���X�y�N�^�[�Ń��[�h����V�[���̖��O��ݒ�
    public TextMeshProUGUI countdownText; // �C���X�y�N�^�[��CountdownText UI�I�u�W�F�N�g��ݒ�
    public GaugeController gaugeController; // �C���X�y�N�^�[��GaugeController�̎Q�Ƃ�ݒ�

    private float timeRemaining = 60f; // 60�b�i1���j

    void Update()
    {
        // �X���C�_�[�������Ă��Ȃ��Ƃ��Ƀ^�C�}�[��i�߂�
        if (gaugeController == null || !gaugeController.IsFilling())
        {
            // �c�莞�Ԃ����炷
            timeRemaining -= Time.deltaTime;
        }

        // �c�莞�Ԃ���ʂɕ\��
        countdownText.text = "�c�莞��: " + Mathf.Ceil(timeRemaining).ToString() + "�b";

        // ���Ԃ��Ȃ��Ȃ�����V�[����؂�ւ���
        if (timeRemaining <= 0)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
