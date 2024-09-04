using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 originalPosition;
    private CanvasGroup canvasGroup;

    // snapToPosition を外部から設定可能にする
    private Transform snapToPosition;

    // ドラッグ終了時のコールバック
    public Action<MonoBehaviour, Action> onDropSuccess;
    public Action<Action> onDropFail;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        // 初期位置を記録
        originalPosition = transform.position;
    }

    // 外部から snapToPosition を設定するメソッド
    public void SetSnapPosition(Transform snapTransform)
    {
        snapToPosition = snapTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }

        if (snapToPosition != null)
        {
            transform.position = snapToPosition.position;

            // DropAreaManager などに通知して処理を行う
            // ここでは例として onDropSuccess を呼び出します
            // 実際の実装に応じて適切に設定してください
            // 例えば、DropArea を判定してそのコールバックを呼び出すなど
            onDropSuccess?.Invoke(null, null);
        }
        else
        {
            // snapToPosition が設定されていない場合は元の位置に戻す
            transform.position = originalPosition;

            onDropFail?.Invoke(() => transform.position = originalPosition);
        }
    }
}
