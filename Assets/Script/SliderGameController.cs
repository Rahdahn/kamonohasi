using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;

public class SliderGameController : MonoBehaviour
{
    public Slider slider;
    public UnityEngine.UI.Image successImage;

    private int currentSliderIndex = 1;
    private float successRangeMin;
    private float successRangeMax;

    void Start()
    {
        // �����̐����͈͂�ݒ�
        SetSuccessRange();

        // �X���C�_�[�ړ��̃R���[�`�����J�n
        StartCoroutine(MoveSlider());
    }

    void SetSuccessRange()
    {
        // �����͈͂������_���Ɍ���
        int randomValue = UnityEngine.Random.Range(2, 10); // 2����9�̊ԂŃ����_���l���擾
        successRangeMin = randomValue - 1;
        successRangeMax = randomValue + 1;

        // �����͈͂��f�o�b�O���O�ɕ\��
        UnityEngine.Debug.Log("Success Range: " + successRangeMin + " to " + successRangeMax);

        // �����͈͂�successImage��z�u
        float normalizedMin = (successRangeMin - slider.minValue) / (slider.maxValue - slider.minValue);
        float normalizedMax = (successRangeMax - slider.minValue) / (slider.maxValue - slider.minValue);
        float minPosition = slider.fillRect.position.x + normalizedMin * slider.fillRect.rect.width;
        float maxPosition = slider.fillRect.position.x + normalizedMax * slider.fillRect.rect.width;

        successImage.rectTransform.position = new Vector3((minPosition + maxPosition) / 2, successImage.rectTransform.position.y, successImage.rectTransform.position.z);
        successImage.rectTransform.sizeDelta = new Vector2(maxPosition - minPosition, successImage.rectTransform.sizeDelta.y);
        successImage.gameObject.SetActive(true); // �����͈͉摜��\��
    }

    IEnumerator MoveSlider()
    {
        float sliderValue = 0f;
        float direction = 1f; // 1�͉E�Ɉړ��A-1�͍��Ɉړ����Ӗ�����

        while (true)
        {
            // �X���C�_�[�̒l���X�V
            sliderValue += Time.deltaTime * 2f * direction; // ���x�𒲐�
            slider.value = sliderValue;

            // �X���C�_�[���[�ɒB�����������ύX
            if (sliderValue >= slider.maxValue || sliderValue <= slider.minValue)
            {
                direction *= -1f; // ������ύX
            }

            // �����͈͓��̃N���b�N���`�F�b�N
            if (Input.GetMouseButtonDown(0))
            {
                float clickPosition = Input.mousePosition.x;

                if (clickPosition >= slider.fillRect.position.x && clickPosition <= slider.fillRect.position.x + slider.fillRect.rect.width)
                {
                    float normalizedClick = (clickPosition - slider.fillRect.position.x) / slider.fillRect.rect.width;
                    float actualValue = Mathf.Lerp(slider.minValue, slider.maxValue, normalizedClick);

                    if (actualValue >= successRangeMin && actualValue <= successRangeMax)
                    {
                        // ���������N���b�N�A���̃X���C�_�[�܂��͌��ʃV�[���ֈړ�
                        if (currentSliderIndex < 4)
                        {
                            currentSliderIndex++;
                            SetSuccessRange(); // ���̃X���C�_�[�̂��߂ɐV���������͈͂�ݒ�
                        }
                        else
                        {
                            SceneManager.LoadScene("ResultScene"); // ���ʃV�[�������[�h
                        }
                    }
                    else
                    {
                        SceneManager.LoadScene("ResultButScene"); // ���ʂł����s�V�[�������[�h
                    }
                }
            }

            yield return null;
        }
    }
}
