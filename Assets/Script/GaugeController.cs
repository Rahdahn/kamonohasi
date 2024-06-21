using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider slider;  // �X���C�_�[���A�^�b�`����
    public float minSpeed = 0.5f;  // �ŏ����x
    public float maxSpeed = 3.0f;  // �ő呬�x
    public float successThreshold = 0.8f;  // �����Ɣ��肷���̐��l
    private bool isFilling = true;  // �X���C�_�[�������Ă��邩�ǂ���
    private bool isIncreasing = true;  // �X���C�_�[�̒l���������Ă��邩�ǂ���
    private GameObject lastClickedObject;  // �Ō�Ƀ^�b�v���ꂽ�I�u�W�F�N�g���L�^����
    private int judgeCount = 0;  // ����񐔂�ǐՂ���ϐ�

    void Update()
    {
        if (slider.gameObject.activeInHierarchy)
        {
            if (isFilling)
            {
                float speed = Mathf.Lerp(minSpeed, maxSpeed, slider.value);  // �X���C�_�[�̒l�Ɋ�Â��đ��x�𒲐�

                if (isIncreasing)
                {
                    slider.value += speed * Time.deltaTime;
                    if (slider.value >= slider.maxValue)
                    {
                        isIncreasing = false;  // �ő�l�ɒB�����猸���ɐ؂�ւ���
                    }
                }
                else
                {
                    slider.value -= speed * Time.deltaTime;
                    if (slider.value <= slider.minValue)
                    {
                        isIncreasing = true;  // �ŏ��l�ɒB�����瑝���ɐ؂�ւ���
                    }
                }
            }

            // �^�b�v���ꂽ�I�u�W�F�N�g���L�^
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                             Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);
                if (hit && hit.collider != null)
                {
                    lastClickedObject = hit.collider.gameObject;
                }
            }

            // ���N���b�N������A�L�^���ꂽ�I�u�W�F�N�g���A�N�e�B�u�ȏꍇ�ɂ̂ݑ�����󂯕t����
            if (Input.GetMouseButtonDown(0) && lastClickedObject != null && lastClickedObject.activeInHierarchy)
            {
                isFilling = !isFilling;  // ���N���b�N�œ���̒�~�ƍĊJ��؂�ւ���

                // 2��ڂ̔���Ő��������s�̔�����s��
                if (!isFilling)
                {
                    judgeCount++;  // ����񐔂𑝉�������

                    if (judgeCount == 2)
                    {
                        if (slider.value >= successThreshold)
                        {
                            Debug.Log("Success!");  // ���������ꍇ�̏���
                            AwardItem();  // �A�C�e�����l������
                            lastClickedObject.SetActive(false);  // �������ɃI�u�W�F�N�g���A�N�e�B�u�ɂ���
                        }
                        else
                        {
                            Debug.Log("Failure.");  // ���s�����ꍇ�̏���
                        }

                        slider.gameObject.SetActive(false);  // 2��ڂ̔���ŃX���C�_�[���A�N�e�B�u�ɂ���
                    }
                }
            }
        }
    }

    // �A�C�e�����l�����邽�߂̊֐�
    void AwardItem()
    {
        // �����ɃA�C�e�����l�����鏈����ǉ�
        // �Ⴆ�΁A�C���x���g���ɃA�C�e����ǉ�����A�|�C���g�𑝂₷�Ȃ�
        Debug.Log("Item awarded!");
    }

    // �X���C�_�[���A�N�e�B�u�ɂȂ������ɏ��������ē����o��
    void OnEnable()
    {
        slider.value = slider.minValue;  // �X���C�_�[�̒l��������
        isFilling = true;  // �X���C�_�[�̓�����J�n
        isIncreasing = true;  // �X���C�_�[�̑���������������
        judgeCount = 0;  // ����񐔂����Z�b�g
    }
}
