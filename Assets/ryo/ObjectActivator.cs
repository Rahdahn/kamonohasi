using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    private bool isActive = true;  // オブジェクトがアクティブかどうかを追跡
    public MovementController[] movementControllers;  // MovementControllerの配列

    void Update()
    {
        // マウスクリックを検出し、クリックされたオブジェクトがこのスクリプトに関連付けられているか確認
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

        // オブジェクト自体のアクティブ状態を切り替える
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
