using UnityEngine;

public class ActiveSwitch : MonoBehaviour
{
    public GameObject[] targetObjects;  // �؂�ւ��Ώۂ̃I�u�W�F�N�g�̔z��
    private ActiveManager activeManager;

    void Start()
    {
        // �^�[�Q�b�g�I�u�W�F�N�g���ݒ肳��Ă��Ȃ��ꍇ�A�������g���^�[�Q�b�g�Ƃ���
        if (targetObjects == null || targetObjects.Length == 0)
        {
            targetObjects = new GameObject[] { gameObject };
        }

        // ActiveManager��T��
        activeManager = FindObjectOfType<ActiveManager>();
        if (activeManager == null)
        {
            Debug.LogError("ActiveManager not found in the scene!");
        }
    }

    void Update()
    {
        // �}�E�X�N���b�N�����o���A�N���b�N���ꂽ�I�u�W�F�N�g��BoxCollider2D�������Ă��邩�m�F
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);
            if (hit.collider != null && hit.collider is BoxCollider2D)
            {
                ToggleActiveState();
            }
        }
    }

    private void ToggleActiveState()
    {
        // �^�[�Q�b�g�I�u�W�F�N�g�̃A�N�e�B�u��Ԃ𔽓]
        foreach (var obj in targetObjects)
        {
            if (obj != null)
            {
                bool newState = !obj.activeSelf;
                if (!newState && activeManager != null)
                {
                    // ��A�N�e�B�u�ɂ���O�Ƀ}�l�[�W���[�ɓo�^
                    activeManager.RegisterInactiveObject(obj.GetComponent<GaugeController>());
                }
                obj.SetActive(newState);
            }
        }
    }
}
