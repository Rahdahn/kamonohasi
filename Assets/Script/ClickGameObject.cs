using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedGameObject : MonoBehaviour
{
    GameObject clickedGameObject; // �N���b�N���ꂽ�Q�[���I�u�W�F�N�g��������ϐ�
    bool isInsideCheckpoint = false; // �`�F�b�N�|�C���g�ɂ��邩�ǂ����������t���O

    // �`�F�b�N�|�C���g�ɓ������Ƃ��ɌĂяo�����֐�
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            isInsideCheckpoint = true;
            Debug.Log("Entered checkpoint: " + other.gameObject.name); // �`�F�b�N�|�C���g�ɓ������Ƃ��̃f�o�b�O���O
        }
    }

    // �`�F�b�N�|�C���g����o���Ƃ��ɌĂяo�����֐�
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            isInsideCheckpoint = false;
            Debug.Log("Exited checkpoint: " + other.gameObject.name); // �`�F�b�N�|�C���g����o���Ƃ��̃f�o�b�O���O
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
                Debug.Log(clickedGameObject.name); // �Q�[���I�u�W�F�N�g�̖��O���o��
                Destroy(clickedGameObject); // �Q�[���I�u�W�F�N�g��j��
            }
        }
    }
}
