using UnityEngine;

public class kurukuru : MonoBehaviour
{
    private Vector3 lastPosition;
    public float minimumMovementThreshold = 0.01f; // ���̒l�ȏ�ړ������ꍇ�ɂ̂݌������X�V

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        float horizontalMove = currentPosition.x - lastPosition.x;

        if (Mathf.Abs(horizontalMove) > minimumMovementThreshold) // ������x�̈ړ����������ꍇ�ɂ̂ݎ��s
        {
            if (horizontalMove > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // �E����
            }
            else if (horizontalMove < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // ������
            }
        }

        lastPosition = currentPosition; // ���݂̈ʒu���X�V
    }
}
