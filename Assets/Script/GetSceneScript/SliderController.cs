using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public float fillSpeed = 0.1f; // �X���C�_�[�̒l��ω������鑬�x

    private bool isFilling = false;

    void Update()
    {
        if (isFilling)
        {
            if (slider.value < slider.maxValue)
            {
                slider.value += fillSpeed * Time.deltaTime;
            }
            else
            {
                isFilling = false;
            }
        }
        else
        {
            if (slider.value > slider.minValue)
            {
                slider.value -= fillSpeed * Time.deltaTime;
            }
        }
    }

    public void StartFilling()
    {
        isFilling = true;
    }

    public void StopFilling()
    {
        isFilling = false;
    }
}
