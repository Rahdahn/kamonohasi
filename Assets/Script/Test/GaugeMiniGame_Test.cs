using UnityEngine;

public class GaugeMiniGame_Test : MonoBehaviour
{
    public GaugeController_Test gaugeController;
    public float successThreshold = 0.1f;  // �����͈͂�臒l�i0.1��10%�͈̔́j

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckSuccess();
        }
    }

    void CheckSuccess()
    {
        if (gaugeController == null || gaugeController.gaugeImage == null)
        {
            Debug.LogError("GaugeController or Gauge Image is not assigned!");
            return;
        }

        // ���݂̃Q�[�W�̒l
        float fillAmount = gaugeController.gaugeImage.fillAmount;

        float successRangeMin = 0.75f - successThreshold;
        float successRangeMax = 0.75f + successThreshold;

        // �Q�[�W�������͈͓����ǂ������`�F�b�N
        if (fillAmount >= successRangeMin && fillAmount <= successRangeMax)
        {
            Debug.Log("����!");
        }
        else
        {
            Debug.Log("���s!");
        }
    }
}
