using System.Collections;
using UnityEngine;

public class AnimalMover : MonoBehaviour
{
    public Transform startLocation;
    public Transform goalLocation;
    public Transform[] relayPoints;
    public float speed = 5f;
    public float cooldownTime = 2f;

    private Transform target;
    private int currentRelayPointIndex = 0;
    private bool isMoving = true;

    void Start()
    {
        // �ŏ��̖ڕW���X�^�[�g�n�_�ɐݒ�
        target = startLocation;
    }

    void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(RespawnAfterCooldown()); // �ړ����I�����Ă���ꍇ�̓��X�|�[���������J�n
            return;
        }

        // �ڕW�Ɍ������Ĉړ�
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // �ڕW�ɓ��B�����玟�̖ڕW��ݒ�
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            if (target == startLocation)
            {
                // �X�^�[�g�n�_�ɓ��B�����璆�p�n�_�ɐ؂�ւ�
                if (relayPoints.Length > 0)
                {
                    currentRelayPointIndex = 0;
                    target = relayPoints[currentRelayPointIndex];
                }
                else
                {
                    target = goalLocation;
                }
            }
            else if (target == goalLocation)
            {
                // �S�[���n�_�ɓ��B������ړ����I��
                isMoving = false;
                StartCoroutine(RespawnAfterCooldown());
            }
            else if (ArrayIsValidIndex(currentRelayPointIndex, relayPoints))
            {
                // ���p�n�_�ɓ��B�����玟�̒��p�n�_�ɐ؂�ւ��i�Ō�̒��p�n�_�̏ꍇ�̓S�[���n�_�ɐ؂�ւ��j
                currentRelayPointIndex++;
                if (ArrayIsValidIndex(currentRelayPointIndex, relayPoints))
                {
                    target = relayPoints[currentRelayPointIndex];
                }
                else
                {
                    target = goalLocation;
                }
            }
        }
    }

    // �z��̗L���ȃC���f�b�N�X���ǂ������m�F
    bool ArrayIsValidIndex(int index, Transform[] array)
    {
        return index >= 0 && index < array.Length;
    }

    // ���X�|�[���������J�n����R���[�`��
    IEnumerator RespawnAfterCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);

        // �I�u�W�F�N�g���A�N�e�B�u�ɂ��ď����ʒu�ɖ߂�
        gameObject.SetActive(false);
        transform.position = startLocation.position;

        // �N�[���_�E�����I�������I�u�W�F�N�g���ĂуA�N�e�B�u�ɂ���
        gameObject.SetActive(true);

        // �ړ����ĊJ
        isMoving = true;
        currentRelayPointIndex = 0;
        target = startLocation;
    }
}
