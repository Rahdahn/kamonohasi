using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // スライダー用の名前空間を追加
using UnityEngine.SceneManagement;

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

    // ドロップエリアごとにドロップされたオブジェクトのタグを保持する辞書
    private Dictionary<MonoBehaviour, string> dropAreaTags;

    void Start()
    {
        dropAreaOccupied = new Dictionary<MonoBehaviour, bool>
        {
            { dropArea1, false },
            { dropArea2, false },
            { dropArea3, false },
            { dropArea4, false }
        };

        dropAreaTags = new Dictionary<MonoBehaviour, string>
        {
            { dropArea1, null },
            { dropArea2, null },
            { dropArea3, null },
            { dropArea4, null }
        };

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
                    // オブジェクトをスナップ位置に移動
                    dropObj.transform.position = GetSnapPosition(area);

                    // ドロップエリアにドロップされたオブジェクトのタグを記録
                    dropAreaTags[area] = dropObj.tag;

                    CollectibleManager.Instance.AddCollectedImage(dropObj.tag);

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

        // すべてのエリアが埋まっている場合、ドロップ順序を保存してsliderMove1を起動
        SaveDropOrder();
        StartsliderMove1();
    }

    void SaveDropOrder()
    {
        List<string> dropOrder = new List<string>
        {
<<<<<<< HEAD
            dropAreaTags[dropArea1],
            dropAreaTags[dropArea2],
            dropAreaTags[dropArea3],
            dropAreaTags[dropArea4]
=======
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
            { "D3,D1,D4,D5", 100.0f },
            { "D3,D1,D5,D4", 95.2f },
            { "D3,D4,D1,D5", 55.7f },
            { "D3,D4,D5,D1", 2.3f },
            { "D3,D5,D4,D1", 91.6f },
            { "D3,D5,D1,D4", 80.3f }
>>>>>>> 457fdfa6479a51bd306bec74465d155b2d58faa0
        };

        string dropOrderString = string.Join(",", dropOrder);
        PlayerPrefs.SetString("DropOrder", dropOrderString);
        PlayerPrefs.SetFloat("DropOrderPercentage", CalculatePercentage(dropOrderString));
    }

    float CalculatePercentage(string dropOrder)
    {
        // ここにパーセンテージを計算するロジックを追加
        // 例: 任意の順序に応じてパーセンテージを設定する
        switch (dropOrder)
        {
            case "D1,D5,D4,D3": return 41.1f;
            case "D1,D5,D3,D4": return 92.2f;
            case "D1,D3,D4,D5": return 90.0f;
            case "D1,D3,D5,D4": return 75.4f;
            case "D1,D4,D5,D3": return 33.3f;
            case "D1,D4,D3,D5": return 8.2f;
            case "D5,D1,D4,D3": return 92.1f;
            case "D5,D1,D3,D4": return 47.1f;
            case "D5,D4,D1,D3": return 8.6f;
            case "D5,D4,D3,D1": return 70.6f;
            case "D5,D3,D1,D4": return 70.0f;
            case "D5,D3,D4,D1": return 34.2f;
            case "D4,D5,D3,D1": return 81.6f;
            case "D4,D5,D1,D3": return 58.8f;
            case "D4,D1,D3,D5": return 5.1f;
            case "D4,D1,D5,D3": return 33.8f;
            case "D4,D3,D1,D5": return 79.7f;
            case "D4,D3,D5,D1": return 53.4f;
            case "D3,D1,D4,D5": return 99.9f;
            case "D3,D1,D5,D4": return 95.2f;
            case "D3,D4,D1,D5": return 55.7f;
            case "D3,D4,D5,D1": return 2.3f;
            case "D3,D5,D4,D1": return 91.6f;
            case "D3,D5,D1,D4": return 80.3f;
            default: return 0.0f;
        }
    }

    void StartsliderMove1()
    {
        sliderMove1.gameObject.SetActive(true); // sliderMove1をアクティブにする
    }
}
