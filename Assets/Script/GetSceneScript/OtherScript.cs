using UnityEngine;

public class OtherScript : MonoBehaviour
{
    public RandomMover randomMover;

    void Update()
    {
        // 成功判定の条件 (例: スペースキーが押されたとき)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            randomMover.OnSuccess();
        }
    }
}
