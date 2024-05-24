using System;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // ドロップ可能オブジェクト
    public Draggable dropObj;

    // ドロップエリア1と2（エディタでアタッチ）
    public MonoBehaviour dropArea1;
    public MonoBehaviour dropArea2;

    void Start()
    {
        // ドロップエリアとスナップ位置の設定
        dropObj.snapPositions = new Dictionary<MonoBehaviour, Vector2>
        {
            { dropArea1, new Vector2(100, 100) }, // スナップ位置1
            { dropArea2, new Vector2(200, 200) }  // スナップ位置2
        };

        dropObj.beforeBeginDrag = () =>
        {
            Debug.Log("ドラッグ前に呼び出される処理");
        };
        dropObj.onDropSuccess = (MonoBehaviour area, Action resetAction) =>
        {
            Debug.Log("ドラッグ成功時に呼び出される処理");
            // resetAction.Invoke(); // スナップさせるため、ここではリセットを呼び出さない
        };
        dropObj.onDropFail = (Action resetAction) =>
        {
            Debug.Log("ドラッグ失敗時に呼び出される処理");
            resetAction.Invoke();
        };
    }
}
