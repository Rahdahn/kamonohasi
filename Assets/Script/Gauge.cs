using UnityEngine;
using System.Collections;

public class Gauge : MonoBehaviour
{
    public CircularGauge gauge;
    public float increaseAmount = 0.1f;
    public float increaseInterval = 0.5f;
    private bool isIncreasing = true;
    private bool isActive = true;  // スクリプトがアクティブかどうかを追跡

    public MovementController[] movementControllers;  // MovementControllerの配列

    void Start()
    {
        if (gauge == null)
        {
            Debug.LogError("CircularGauge is not assigned!");
            return;
        }

        StartCoroutine(IncreaseGaugeOverTime());
    }

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

    IEnumerator IncreaseGaugeOverTime()
    {
        while (true)
        {
            if (isActive && isIncreasing)
            {
                gauge.IncreaseGauge(increaseAmount);
                if (gauge.CurrentValue >= gauge.maxValue)
                {
                    isIncreasing = false;
                }
            }
            yield return new WaitForSeconds(increaseInterval);
        }
    }

    private void ToggleActiveState()
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
    }
}
