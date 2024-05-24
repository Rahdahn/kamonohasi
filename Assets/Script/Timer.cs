using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;
    public Button startButton;  // �J�n�{�^�����Q�Ƃ��邽�߂̕ϐ�
    public GameObject[] uiElementsToHide;  // ��\���ɂ���UI�v�f�̔z��
    public GameObject displayObject; // �\������I�u�W�F�N�g

    private void Start()
    {
        // �{�^���̃N���b�N�C�x���g�Ƀ��X�i�[��ǉ�
        startButton.onClick.AddListener(StartTimerOnClick);
        DisplayTime(timeRemaining);  // �������Ԃ�\��
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                // �w��̎��Ԃ��o�߂������ʂ�\������R���[�`�����J�n
                StartCoroutine(DisplayTimeout());
            }
        }
    }

    // �{�^���N���b�N�Ń^�C�}�[���J�n���郁�\�b�h
    private void StartTimerOnClick()
    {
        if (!timerIsRunning) // �^�C�}�[�����ɓ��쒆�łȂ��ꍇ�ɊJ�n
        {
            timerIsRunning = true;
            Debug.Log("Timer started by button click.");
            HideUIElements();
        }
    }

    private IEnumerator DisplayTimeout()
    {
        yield return new WaitForSeconds(0f); // 2�b�ҋ@

        // ��ʂ�\������I�u�W�F�N�g���A�N�e�B�u�ɂ���
        displayObject.SetActive(true);
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

      

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void HideUIElements()
    {
        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(false);  // UI�v�f���\���ɂ���
        }
    }
}
