using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Vector2 moveableAreaSize = new Vector2(5f, 3f); // �ړ��\�Ȕ͈͂̃T�C�Y

    private Vector2 minPosition; // �ŏ��̈ړ��\�ʒu
    private Vector2 maxPosition; // �ő�̈ړ��\�ʒu

    void Start()
    {
        // �ŏ��ƍő�̈ʒu���v�Z����
        minPosition = new Vector2(transform.position.x - moveableAreaSize.x / 2f, transform.position.y - moveableAreaSize.y / 2f);
        maxPosition = new Vector2(transform.position.x + moveableAreaSize.x / 2f, transform.position.y + moveableAreaSize.y / 2f);
    }

    void OnMouseDown()
    {
        // �h���b�O���J�n����ۂɕK�v�ȏ�����ǉ�
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        // �ړ��\�Ȕ͈͓��ł̂݃I�u�W�F�N�g���ړ�������
        float clampedX = Mathf.Clamp(mousePosition.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(mousePosition.y, minPosition.y, maxPosition.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void OnMouseUp()
    {
        // �h���b�O���I������ۂɕK�v�ȏ�����ǉ�
    }

    // �͈͂̃T�C�Y���ύX���ꂽ�ꍇ�ɌĂяo����鏈��
    void OnValidate()
    {
        // �ŏ��ƍő�̈ʒu���Čv�Z����
        minPosition = new Vector2(transform.position.x - moveableAreaSize.x / 2f, transform.position.y - moveableAreaSize.y / 2f);
        maxPosition = new Vector2(transform.position.x + moveableAreaSize.x / 2f, transform.position.y + moveableAreaSize.y / 2f);
    }

    // �ړ��\�Ȕ͈͂��������߂� Gizmos ���g�p
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(moveableAreaSize.x, moveableAreaSize.y, 0f));
    }
}
