using System;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float moveSpeed = 2f;        // �����̈ړ����x
    public float changeDirectionTime = 2f; // ������ς���܂ł̎���

    private Vector2 targetPosition;    // �������ړ�����ڕW�n�_
    private float timer;               // �����ύX�̃^�C�}�[
    private bool isMoving = true;      // �ړ���Ԃ𐧌䂷��t���O

    private void Start()
    {
        SetRandomTargetPosition(); // �����̖ڕW�n�_��ݒ�
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

    private void SetRandomTargetPosition()
    {
        // �J�����̃r���[�|�[�g�T�C�Y���擾
        Camera cam = Camera.main;
        Vector2 minBounds = cam.ViewportToWorldPoint(new Vector2(0.1f, 0.1f)); // �����̃��[���h���W
        Vector2 maxBounds = cam.ViewportToWorldPoint(new Vector2(0.9f, 0.9f)); // �E��̃��[���h���W

        // �r���[�|�[�g���Ń����_���ȖڕW�n�_��ݒ�
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
