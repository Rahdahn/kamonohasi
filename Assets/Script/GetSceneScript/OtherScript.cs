using UnityEngine;

public class OtherScript : MonoBehaviour
{
    public RandomMover randomMover;

    void Update()
    {
        // ��������̏��� (��: �X�y�[�X�L�[�������ꂽ�Ƃ�)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            randomMover.OnSuccess();
        }
    }
}
