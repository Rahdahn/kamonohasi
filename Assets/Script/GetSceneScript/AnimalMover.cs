using System.Collections;
using UnityEngine;

public class AnimalMover : MonoBehaviour
{
    public Transform startLocation;
    public Transform goalLocation;
    public Transform[] relayPoints;
    public float speed = 5f;
    public float cooldownTime = 2f;

    // ���X�|�[���Ώۂ̃I�u�W�F�N�g���w�肷��t�B�[���h
    public Transform[] objectsToRespawn;

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
        // �I�u�W�F�N�g�̓������ꎞ�I�ɒ�~
        foreach (var obj in objectsToRespawn)
        {
            if (obj != null)
            {
                // �������~���邽�߂̏�����ǉ��i��: ���x��0�ɂ���Ȃǁj
                var mover = obj.GetComponent<AnimalMover>();
                if (mover != null)
                {
                    mover.StopMoving(); // ��~���\�b�h���Ăяo��
                }
            }
        }

        // �N�[���_�E���̑ҋ@
        yield return new WaitForSeconds(cooldownTime);

        // �w�肳�ꂽ�I�u�W�F�N�g���A�N�e�B�u�ɂ��ď����ʒu�ɖ߂�
        foreach (var obj in objectsToRespawn)
        {
            if (obj != null)
            {
                obj.gameObject.SetActive(false);
                obj.position = startLocation.position;
                obj.gameObject.SetActive(true);

                // �������ĊJ���邽�߂̏�����ǉ��i��: ���x�����ɖ߂��Ȃǁj
                var mover = obj.GetComponent<AnimalMover>();
                if (mover != null)
                {
                    mover.StartMoving(); // �ĊJ���\�b�h���Ăяo��
                }
            }
        }

        // �I�u�W�F�N�g�̓������ĊJ
        isMoving = true;
        currentRelayPointIndex = 0;
        target = startLocation;
    }

    // ����������󂯎�����ۂɌĂ΂�郁�\�b�h
    public void OnGaugeSuccess()
    {
        // �ړ����~
        StopMoving();

        // ���X�|�[���������J�n
        StartCoroutine(RespawnAfterCooldown());
    }

    // �I�u�W�F�N�g�̓������~���郁�\�b�h
    public void StopMoving()
    {
        isMoving = false; // �ړ��t���O���~
        speed = 0f; // �������~����
    }

    // �I�u�W�F�N�g�̓������ĊJ���郁�\�b�h
    public void StartMoving()
    {
        isMoving = true; // �ړ��t���O���ĊJ
        speed = 5f; // ���̑��x�ɖ߂�
    }
}
