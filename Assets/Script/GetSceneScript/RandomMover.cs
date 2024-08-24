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
            Debug.LogError("Target Object ���w�肳��Ă��܂���B");
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

    IEnumerator SetupMovement()
    {
        while (true)
        {
            if (targetObject != null && !isPaused)
            {
                targetPosition = GetRandomPosition();
                isMoving = true;
                yield return new WaitForSeconds(moveInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(-moveRangeX, moveRangeX);
        float randomY = Random.Range(-moveRangeY, moveRangeY);
        return new Vector2(originalPosition.x + randomX, originalPosition.y + randomY);
    }

    IEnumerator SetRandomTargetPosition()
    {
        if (!isPaused)
        {
            targetPosition = GetRandomPosition();
        }
        yield return null;
    }

    public void OnSuccess()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        if (targetObject != null)
        {
            isMoving = false;
            Debug.Log("�^�[�Q�b�g�I�u�W�F�N�g����ʊO�Ɉړ����܂����B");

            Vector2 offScreenPosition = (Vector2)targetObject.transform.position + offScreenOffset;
            targetObject.transform.position = offScreenPosition;

            yield return new WaitForSeconds(respawnDelay);

            Debug.Log("�^�[�Q�b�g�I�u�W�F�N�g�����̈ʒu�ɖ߂�܂����B");
            targetObject.transform.position = originalPosition;
            StartCoroutine(SetupMovement());
        }
    }

    public void PauseMovement()
    {
        isPaused = true;
        isMoving = false;
        Debug.Log("�ړ����ꎞ��~����܂����B");
    }

    public void ResumeMovement()
    {
        isPaused = false;
        isMoving = true;
        Debug.Log("�ړ����ĊJ����܂����B");
        StartCoroutine(SetupMovement()); // �Ăшړ����J�n
    }

    // �}�E�X�N���b�N�����m���ă|�[�Y��Ԃ�؂�ւ���
    void OnMouseDown()
    {
        if (isPaused)
        {
            ResumeMovement();
        }
        else
        {
            PauseMovement();
        }
    }

    void OnDrawGizmos()
    {
        if (targetObject != null)
        {
            Gizmos.color = Color.green;
            Vector3 gizmoCenter = new Vector3(originalPosition.x, originalPosition.y, 0) + new Vector3(gizmoOffset.x, gizmoOffset.y, 0);
            Gizmos.DrawWireCube(gizmoCenter, new Vector3(moveRangeX * 2, moveRangeY * 2, 0));
        }
    }
}
