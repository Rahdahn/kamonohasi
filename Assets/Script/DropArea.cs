using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // スライダー用の名前空間を追加

public class Test : MonoBehaviour
{
    // ドロップ可能オブジェクトリスト
    public List<Draggable> dropObjs;

    // ドロップエリア（エディタでアタッチ）
    public MonoBehaviour dropArea1;
    public MonoBehaviour dropArea2;
    public MonoBehaviour dropArea3;
    public MonoBehaviour dropArea4;

    // MixGaugeManager（エディタでアタッチ）
    public MixGaugeManager mixGaugeManager;

    // 各ドロップエリアの状態を追跡する辞書
    private Dictionary<MonoBehaviour, bool> dropAreaOccupied;

    void Start()
    {
        dropAreaOccupied = new Dictionary<MonoBehaviour, bool>
        {
            { dropArea1, false },
            { dropArea2, false },
            { dropArea3, false },
            { dropArea4, false }
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

            dropObj.beforeBeginDrag = () =>
            {
                Debug.Log("ドラッグ前に呼び出される処理");
            };
            dropObj.onDropSuccess = (MonoBehaviour area, Action resetAction) =>
            {
                Debug.Log("ドラッグ成功時に呼び出される処理");

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

                    // すべてのドロップエリアが埋まったかチェック
                    CheckAllDropAreasFilled();
                }
            };
            dropObj.onDropFail = (Action resetAction) =>
            {
                Debug.Log("ドラッグ失敗時に呼び出される処理");
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
        // すべてのエリアが埋まっている場合、MixGaugeManagerを起動
        StartMixGaugeManager();
    }

    void StartMixGaugeManager()
    {
        mixGaugeManager.gameObject.SetActive(true); // MixGaugeManagerをアクティブにする
        mixGaugeManager.Initialize(); // MixGaugeManagerのInitializeメソッドを呼び出す
    }
}
