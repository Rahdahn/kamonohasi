using UnityEngine;

public class ActiveSwitch : MonoBehaviour
{
    public GameObject targetObject;
    private ActiveManager activeManager;

    void Start()
    {
        // targetObjectが設定されていない場合、自分自身をターゲットとする
        if (targetObject == null)
        {
            targetObject = gameObject;
        }

        // ActiveManagerを探す
        activeManager = FindObjectOfType<ActiveManager>();
        if (activeManager == null)
        {
            Debug.LogError("ActiveManager not found in the scene!");
        }
    }

    void Update()
    {
        // マウスクリックを検出し、クリックされたオブジェクトがBoxCollider2Dを持っているか確認
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
        // ターゲットオブジェクトのアクティブ状態を反転
        if (targetObject != null)
        {
            bool newState = !targetObject.activeSelf;
            if (!newState && activeManager != null)
            {
                // 非アクティブにする前にマネージャーに登録
                activeManager.RegisterInactiveObject(targetObject);
            }
            targetObject.SetActive(newState);
        }
    }
}
