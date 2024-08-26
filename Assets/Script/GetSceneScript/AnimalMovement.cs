using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float moveSpeed = 2f;        // �����̈ړ����x
    public float changeDirectionTime = 2f; // ������ς���܂ł̎���

    private Vector2 targetPosition;    // �������ړ�����ڕW�n�_
    private float timer;               // �����ύX�̃^�C�}�[

    private void Start()
    {
        SetRandomTargetPosition(); // �����̖ڕW�n�_��ݒ�
    }

    private void Update()
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

    private void SetRandomTargetPosition()
    {
        // �J�����̃r���[�|�[�g�T�C�Y���擾
        Camera cam = Camera.main;
        Vector2 minBounds = cam.ViewportToWorldPoint(new Vector2(0, 0)); // �����̃��[���h���W
        Vector2 maxBounds = cam.ViewportToWorldPoint(new Vector2(1, 1)); // �E��̃��[���h���W

        // �r���[�|�[�g���Ń����_���ȖڕW�n�_��ݒ�
        targetPosition = new Vector2(
            UnityEngine.Random.Range(minBounds.x, maxBounds.x),
            UnityEngine.Random.Range(minBounds.y, maxBounds.y)
        );
    }
}
