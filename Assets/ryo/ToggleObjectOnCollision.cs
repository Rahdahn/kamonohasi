using UnityEngine;

public class ToggleObjectOnAnyBoxColliderClick : MonoBehaviour
{
    // �o��������/��A�N�e�B�u�ɂ���I�u�W�F�N�g
    public GameObject targetObject;

    // ������Ԃł̃I�u�W�F�N�g�̏�Ԃ�ݒ�
    private bool isActive = false;

    void Start()
    {
        // �^�[�Q�b�g�I�u�W�F�N�g���A�N�e�B�u�ɐݒ�
        targetObject.SetActive(isActive);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���N���b�N
        {
            // �}�E�X�ʒu���擾���A���[���h���W�ɕϊ�
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // �}�E�X�ʒu�Ƀ��C���΂��ăq�b�g�����I�u�W�F�N�g���擾
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // �N���b�N���ꂽ�I�u�W�F�N�g��BoxCollider2D�������Ă��邩�m�F
            if (hit.collider != null && hit.collider.GetComponent<BoxCollider2D>() != null)
            {
                // �^�[�Q�b�g�I�u�W�F�N�g�̃A�N�e�B�u��Ԃ�؂�ւ�
                isActive = !isActive;
                targetObject.SetActive(isActive);
            }
        }
    }
}
