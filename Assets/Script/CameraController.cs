using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 2.0f; // �J�����̈ړ����x
    public float stopY = 0.0f; // �J�����̒�~����Y���W

    private bool sceneStarted = false; // �V�[�����n�܂������ǂ����̃t���O

    void Start()
    {
        sceneStarted = true; // �V�[�����n�܂������Ƃ������t���O�𗧂Ă�
    }

    void Update()
    {
        if (sceneStarted)
        {
            // �J���������ɓ�����
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            // �J������Y���W����~�ʒu�𒴂������~����
            if (transform.position.y <= stopY)
            {
                // �J�����̈ʒu�𒼐ڒ�~�ʒu�ɐݒ�
                transform.position = new Vector3(transform.position.x, stopY, transform.position.z);
                sceneStarted = false; // �V�[������~�������Ƃ������t���O�𗧂Ă�
            }
        }

        // �V�[�����n�܂��Ă���Ƃ������}�E�X�N���b�N�𖳎�����
        if (Input.GetMouseButtonDown(0) && sceneStarted)
        {
            return;
        }
    }
}
