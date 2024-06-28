using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    private bool isActive = true;  // �I�u�W�F�N�g���A�N�e�B�u���ǂ�����ǐ�
    public MovementController[] movementControllers;  // MovementController�̔z��

    void Update()
    {
        // �}�E�X�N���b�N�����o���A�N���b�N���ꂽ�I�u�W�F�N�g�����̃X�N���v�g�Ɋ֘A�t�����Ă��邩�m�F
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);
            if (hit && hit.collider != null && hit.collider.gameObject == gameObject)
            {
                ToggleActiveState();
            }
        }
    }

    public void ToggleActiveState()
    {
        isActive = !isActive;

        if (isActive)
        {
            foreach (var movementController in movementControllers)
            {
                if (movementController != null)
                {
                    movementController.ResumeMovement();
                }
            }
        }
        else
        {
            foreach (var movementController in movementControllers)
            {
                if (movementController != null)
                {
                    movementController.PauseMovement();
                }
            }
        }

        // �I�u�W�F�N�g���̂̃A�N�e�B�u��Ԃ�؂�ւ���
        gameObject.SetActive(isActive);
    }

    public void Deactivate()
    {
        if (isActive)
        {
            ToggleActiveState();
        }
    }
}
