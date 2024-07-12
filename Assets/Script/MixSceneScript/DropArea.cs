using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropArea : MonoBehaviour
{
    // ドロップ可能オブジェクトリスト
    public List<Draggable> dropObjs;

    // ドロップエリア（エディタでアタッチ）
    public MonoBehaviour dropArea1;
    public MonoBehaviour dropArea2;
    public MonoBehaviour dropArea3;
    public MonoBehaviour dropArea4;

    // sliderMove1（エディタでアタッチ）
    public SliderMove1 sliderMove1;

    // 各ドロップエリアの状態を追跡する辞書
    private Dictionary<MonoBehaviour, bool> dropAreaOccupied;
    private List<string> dropOrder;

    void Start()
    {
        dropAreaOccupied = new Dictionary<MonoBehaviour, bool>
        {
            { dropArea1, false },
            { dropArea2, false },
            { dropArea3, false },
            { dropArea4, false }
        };

        dropOrder = new List<string>();

        foreach (var dropObj in dropObjs)
        {
            // ドロップエリアとスナップ位置の設定
            dropObj.snapPositions = new Dictionary<MonoBehaviour, Vector2>
            {
                { dropArea1, GetSnapPosition(dropArea1) },
                { dropArea2, GetSnapPosition(dropArea2) },
                { dropArea3, GetSnapPosition(dropArea3) },
                { dropArea4, GetSnapPosition(dropArea4) }
            };

            dropObj.onDropSuccess = (MonoBehaviour area, Action resetAction) =>
            {
                Debug.Log("ドラッグ成功時");

                if (dropAreaOccupied[area])
                {
                    Debug.Log("このエリアはすでに使用されています。");
                    resetAction.Invoke();
                }
                else
                {
                    dropAreaOccupied[area] = true;
                    dropOrder.Add(dropObj.tag); // オブジェクトのタグを順番に追加

                    // オブジェクトをスナップ位置に移動
                    dropObj.transform.position = GetSnapPosition(area);

                    // すべてのドロップエリアが埋まったかチェック
                    CheckAllDropAreasFilled();
                }
            };
            dropObj.onDropFail = (Action resetAction) =>
            {
                Debug.Log("ドラッグ失敗時");
                resetAction.Invoke();
            };
        }
    }

    Vector2 GetSnapPosition(MonoBehaviour dropArea)
    {
        return dropArea.transform.position;
    }

    void CheckAllDropAreasFilled()
    {
        foreach (var occupied in dropAreaOccupied.Values)
        {
            if (!occupied)
            {
                return; // まだ埋まっていないエリアがある場合は終了
            }
        }
        // すべてのエリアが埋まっている場合、順番を保存してsliderMove1を起動
        SaveDropOrderAndStartSliderMove();
    }

    void SaveDropOrderAndStartSliderMove()
    {
        string order = string.Join(",", dropOrder);
        PlayerPrefs.SetString("DropOrder", order);
        PlayerPrefs.SetFloat("DropOrderPercentage", GetPercentageByOrder(order));
        sliderMove1.gameObject.SetActive(true); // sliderMove1をアクティブにする
    }

    float GetPercentageByOrder(string order)
    {
        // 表のパーセンテージを対応付ける辞書を設定
        var percentages = new Dictionary<string, float>
        {
            { "D1,D5,D4,D3", 41.1f },
            { "D1,D5,D3,D4", 92.2f },
            { "D1,D3,D4,D5", 90.0f },
            { "D1,D3,D5,D4", 75.4f },
            { "D1,D4,D5,D3", 33.3f },
            { "D1,D4,D3,D5", 8.2f },
            { "D5,D1,D4,D3", 92.1f },
            { "D5,D1,D3,D4", 47.1f },
            { "D5,D4,D1,D3", 8.6f },
            { "D5,D4,D3,D1", 70.6f },
            { "D5,D3,D1,D4", 70.0f },
            { "D5,D3,D4,D1", 34.2f },
            { "D4,D5,D3,D1", 81.6f },
            { "D4,D5,D1,D3", 58.8f },
            { "D4,D1,D3,D5", 5.1f },
            { "D4,D1,D5,D3", 33.8f },
            { "D4,D3,D1,D5", 79.7f },
            { "D4,D3,D5,D1", 53.4f },
            { "D3,D1,D4,D5", 99.9f },
            { "D3,D1,D5,D4", 95.2f },
            { "D3,D4,D1,D5", 55.7f },
            { "D3,D4,D5,D1", 2.3f },
            { "D3,D5,D4,D1", 91.6f },
            { "D3,D5,D1,D4", 80.3f }
        };

        if (percentages.ContainsKey(order))
        {
            return percentages[order];
        }
        return 0.0f; // デフォルト値
    }
}
