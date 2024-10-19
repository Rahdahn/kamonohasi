using System;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float moveSpeed = 2f;           // �����̈ړ����x
    public float changeDirectionTime = 2f; // ������ς���܂ł̎���

    private Vector2 targetPosition;        // �������ړ�����ڕW�n�_
    private float timer;                   // �����ύX�̃^�C�}�[
    private bool isMoving = true;          // �ړ���Ԃ𐧌䂷��t���O

    private Vector2 minBounds;             // ���̃J�����̍����̃��[���h���W
    private Vector2 maxBounds;             // ���̃J�����̉E��̃��[���h���W

    private void Start()
    {
        // �Y�[���O�̃J�����͈͂��v�Z���ĕۑ�
        SetInitialCameraBounds();

        // �����̖ڕW�n�_��ݒ�
        SetRandomTargetPosition();
    }

    private void Update()
    {
        if (isMoving)
        {
            // �^�C�}�[���X�V���A��莞�Ԍo�ߌ�ɕ�����ς���
            timer += Time.deltaTime;
            if (timer >= changeDirectionTime)
            {
                SetRandomTargetPosition();
                timer = 0;
            }

            // ������ڕW�n�_�Ɍ������Ĉړ�������
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // �ڕW�n�_�ɓ��B������A�V�����ڕW�n�_��ݒ肷��
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                SetRandomTargetPosition();
            }
        }
    }

    // ���̃J�����͈͂��擾���郁�\�b�h
    private void SetInitialCameraBounds()
    {
        Camera cam = Camera.main;

        // �Y�[���O�̃J�����̃r���[�|�[�g�T�C�Y���擾���ĕۑ�
        minBounds = cam.ViewportToWorldPoint(new Vector2(0.1f, 0.1f)); // �����̃��[���h���W
        maxBounds = cam.ViewportToWorldPoint(new Vector2(0.9f, 0.9f)); // �E��̃��[���h���W
    }

    // �����_���ȖڕW�n�_��ݒ肷�郁�\�b�h
    private void SetRandomTargetPosition()
    {
        // ���̃J�����͈͓��Ń����_���ȖڕW�n�_��ݒ�
        targetPosition = new Vector2(
            UnityEngine.Random.Range(minBounds.x, maxBounds.x),
            UnityEngine.Random.Range(minBounds.y, maxBounds.y)
        );
    }

    // �ړ����~���郁�\�b�h
    public void StopMoving()
    {
        isMoving = false;
    }

    // �ړ����ĊJ���郁�\�b�h
    public void StartMoving()
    {
        isMoving = true;
    }
}
