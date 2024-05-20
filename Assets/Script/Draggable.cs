using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // このオブジェクトの元の位置
    private Vector2 prePos;

    // このオブジェクトの元の親
    private GameObject preParent;

    // ドロップ可能エリア
    public List<MonoBehaviour> dropArea;

    // ドラッグ開始時に実行するアクション
    public Action beforeBeginDrag;

    // ドロップ完了時に実行するアクション
    public Action<MonoBehaviour, Action> onDropSuccess;

    // ドロップ可能エリア以外にドロップされたときの処理
    public Action<Action> onDropFail;

    // ドロップエリアにスナップする位置を設定
    public Dictionary<MonoBehaviour, Vector2> snapPositions;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ開始時に実行するアクションを実行
        beforeBeginDrag?.Invoke();

        // このオブジェクトの元の位置と親を予め保存
        prePos = transform.position;
        preParent = transform.parent.gameObject;

        // 最上位に移動
        transform.SetParent(transform.root.gameObject.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Screen Space Cameraの場合、カメラの位置からの差分だけ移動させる
        Vector3 vec = Camera.main.WorldToScreenPoint(transform.position);
        vec.x += eventData.delta.x;
        vec.y += eventData.delta.y;
        transform.position = Camera.main.ScreenToWorldPoint(vec);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool isSuccess = false;
        foreach (MonoBehaviour area in dropArea)
        {
            if (contains(area.GetComponent<RectTransform>(), eventData))
            {
                // ドロップ可能エリアにこのオブジェクトが含まれる場合
                isSuccess = true;
                if (snapPositions != null && snapPositions.ContainsKey(area))
                {
                    // スナップ位置が設定されている場合、その位置に移動
                    transform.position = snapPositions[area];
                }
                onDropSuccess?.Invoke(area, resetPos());
                break;
            }
        }

        // 失敗時処理
        if (!isSuccess)
        {
            if (onDropFail == null)
            {
                // 失敗時アクションが未設定の場合、位置をもとに戻す
                resetPos().Invoke();
            }
            else
            {
                // アクション設定済みならそれを実行
                onDropFail.Invoke(resetPos());
            }
        }
    }

    private Action resetPos()
    {
        return () =>
        {
            // 位置をもとに戻す
            transform.position = prePos;
            transform.SetParent(preParent.transform, true);
        };
    }

    // targetがareaの範囲内にいるかどうかを判定する
    private bool contains(RectTransform area, PointerEventData target)
    {
        var selfBounds = GetBounds(area);
        Vector3 worldPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            area,
            target.position,
            target.pressEventCamera,
            out worldPos);
        worldPos.z = 0f;
        return selfBounds.Contains(worldPos);
    }

    private Bounds GetBounds(RectTransform target)
    {
        Vector3[] s_Corners = new Vector3[4];
        var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        target.GetWorldCorners(s_Corners);
        for (var index2 = 0; index2 < 4; ++index2)
        {
            min = Vector3.Min(s_Corners[index2], min);
            max = Vector3.Max(s_Corners[index2], max);
        }

        max.z = 0f;
        min.z = 0f;

        Bounds bounds = new Bounds(min, Vector3.zero);
        bounds.Encapsulate(max);
        return bounds;
    }
}
