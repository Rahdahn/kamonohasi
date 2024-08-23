using System.Collections;
using UnityEngine;

public class RandomMover : MonoBehaviour
{
    public GameObject targetObject; // �ړ�����I�u�W�F�N�g���w�肷��
    public float moveRangeX = 5.0f; // �������̈ړ��͈�
    public float moveRangeY = 5.0f; // �c�����̈ړ��͈�
    public float moveInterval = 2.0f; // �����_���Ɉړ�����Ԋu�i�b�j
    public float respawnDelay = 3.0f; // ���X�|�[������܂ł̎���
    public float moveSpeed = 2.0f; // �ړ����x

    public Vector2 gizmoOffset = new Vector2(0, 0); // Gizmos �\���̃I�t�Z�b�g
    public Vector2 offScreenOffset = new Vector2(1000, 1000); // ��ʊO�Ɉړ�����I�t�Z�b�g

    private Vector2 originalPosition;
    private Vector2 targetPosition;
    private bool isPaused = false; // �ꎞ��~��Ԃ�ێ�����t���O
    private bool isMoving = false;

    void Start()
    {
        if (targetObject != null)
        {
            originalPosition = targetObject.transform.position;
            StartCoroutine(SetupMovement()); // �������ƈړ��ݒ�
        }
        else
        {
            Debug.LogError("Target Object is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (isPaused || !isMoving) return;

        // �X���[�Y�Ɉړ�
        targetObject.transform.position = Vector2.MoveTowards(targetObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // �ڕW�ʒu�ɓ��B�����玟�̖ڕW�ʒu��ݒ�
        if ((Vector2)targetObject.transform.position == targetPosition)
        {
            StartCoroutine(SetRandomTargetPosition());
        }
    }

    // �����ݒ�ƈړ���ݒ肷��R���[�`��
    IEnumerator SetupMovement()
    {
        while (true)
        {
            if (targetObject != null)
            {
                targetPosition = GetRandomPosition();
                isMoving = true;
                yield return new WaitForSeconds(moveInterval);
            }
            else
            {
                yield break; // targetObject �� null �̏ꍇ�A�R���[�`�����I��
            }
        }
    }

    // �����_���ȖڕW�ʒu���擾
    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(-moveRangeX, moveRangeX);
        float randomY = Random.Range(-moveRangeY, moveRangeY);
        return new Vector2(originalPosition.x + randomX, originalPosition.y + randomY);
    }

    // �ڕW�ʒu��ݒ肷��R���[�`��
    IEnumerator SetRandomTargetPosition()
    {
        targetPosition = GetRandomPosition();
        yield return null; // ���̃t���[���ł̈ړ��J�n
    }

    // ���̃X�N���v�g����Ăяo����鐬��������󂯎�郁�\�b�h
    public void OnSuccess()
    {
        StartCoroutine(Respawn());
    }

    // �I�u�W�F�N�g����ʊO�Ɉړ������A��莞�Ԍ�Ƀ��X�|�[��������R���[�`��
    IEnumerator Respawn()
    {
        if (targetObject != null)
        {
            // �ꎞ��~����
            isMoving = false;
            Debug.Log("Target object moved off-screen.");

            // ��ʊO�Ɉړ�������
            Vector2 offScreenPosition = (Vector2)targetObject.transform.position + offScreenOffset;
            targetObject.transform.position = offScreenPosition;

            // ���X�|�[���܂őҋ@
            yield return new WaitForSeconds(respawnDelay);

            // ���X�|�[������
            Debug.Log("Target object moved back to the original position.");

            // ���̈ʒu�ɖ߂�
            targetObject.transform.position = originalPosition;
            StartCoroutine(SetupMovement()); // �ړ��ĊJ
        }
    }

    // �ꎞ��~�@�\��A�������邽�߂̃��\�b�h
    public void PauseMovement()
    {
        isPaused = true;
        Debug.Log("Movement paused.");
    }

    public void ResumeMovement()
    {
        isPaused = false;
        Debug.Log("Movement resumed.");
    }

    // Gizmos ���g�p���Ĉړ��͈͂�\��
    void OnDrawGizmos()
    {
        if (targetObject != null)
        {
            // �ړ��͈͂̒��S�ʒu��\��
            Gizmos.color = Color.green;
            Vector3 gizmoCenter = new Vector3(originalPosition.x, originalPosition.y, 0) + new Vector3(gizmoOffset.x, gizmoOffset.y, 0);
            Gizmos.DrawWireCube(gizmoCenter, new Vector3(moveRangeX * 2, moveRangeY * 2, 0));
        }
    }
}
