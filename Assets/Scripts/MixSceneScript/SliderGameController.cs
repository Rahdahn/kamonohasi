using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SliderGameController : MonoBehaviour
{
    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public Slider slider4;

    private Slider[] sliders; // �����̃X���C�_�[���Ǘ����邽�߂̔z��

    private int currentSliderIndex = 0;
    private float[] successRangeMin; // �����͈͂̍ŏ��l
    private float[] successRangeMax; // �����͈͂̍ő�l

    public void Start()
    {
        // �X���C�_�[��z��Ɋi�[
        sliders = new Slider[] { slider1, slider2, slider3, slider4 };

        // �����͈͂̏�����
        successRangeMin = new float[sliders.Length];
        successRangeMax = new float[sliders.Length];

        // �e�X���C�_�[���Ƃɐ����͈͂�ݒ�
        successRangeMin[0] = 1f;
        successRangeMax[0] = 2f;

        successRangeMin[1] = 2f;
        successRangeMax[1] = 3f;

        successRangeMin[2] = 3f;
        successRangeMax[2] = 4f;

        successRangeMin[3] = 4f;
        successRangeMax[3] = 5f;

        // �Q�[���J�n���ɏ�����
        Initialize();
    }

    public void Initialize()
    {
        // �ŏ��̃X���C�_�[���A�N�e�B�u�ɂ���
        sliders[currentSliderIndex].gameObject.SetActive(true);

        // �X���C�_�[�ړ��̃R���[�`�����J�n
        StartCoroutine(MoveSlider());
    }

    IEnumerator MoveSlider()
    {
        Slider currentSlider = sliders[currentSliderIndex];
        float sliderValue = currentSlider.minValue;
        float direction = 1f; // 1�͉E�Ɉړ��A-1�͍��Ɉړ����Ӗ�����

        while (true)
        {
            // �X���C�_�[�̒l���X�V
            sliderValue += Time.deltaTime * 2f * direction; // ���x�𒲐�
            currentSlider.value = sliderValue;

            // �X���C�_�[���[�ɒB�����������ύX
            if (sliderValue >= currentSlider.maxValue || sliderValue <= currentSlider.minValue)
            {
                direction *= -1f; // ������ύX
            }

            if (Input.GetMouseButtonDown(0))
            {
                float clickPosition = Input.mousePosition.x;

                if (clickPosition >= currentSlider.fillRect.position.x && clickPosition <= currentSlider.fillRect.position.x + currentSlider.fillRect.rect.width)
                {
                    float normalizedClick = (clickPosition - currentSlider.fillRect.position.x) / currentSlider.fillRect.rect.width;
                    float actualValue = Mathf.Lerp(currentSlider.minValue, currentSlider.maxValue, normalizedClick);

                    // �����͈͓��̃N���b�N�𔻒�
                    if (actualValue >= successRangeMin[currentSliderIndex] && actualValue <= successRangeMax[currentSliderIndex])
                    {
                        // ���̃X���C�_�[�܂��͌��ʃV�[���ֈړ�
                        if (currentSliderIndex < sliders.Length - 1)
                        {
                            currentSlider.gameObject.SetActive(false); // ���݂̃X���C�_�[���A�N�e�B�u�ɂ���
                            currentSliderIndex++;
                            sliders[currentSliderIndex].gameObject.SetActive(true); // ���̃X���C�_�[���A�N�e�B�u�ɂ���
                        }
                        else
                        {
                            SceneManager.LoadScene("ResultScene"); // ���ʃV�[�������[�h
                        }
                    }
                    else
                    {
                        SceneManager.LoadScene("ResultButScene"); // ���s�V�[�������[�h
                    }
                }
            }

            yield return null;
        }
    }
}
