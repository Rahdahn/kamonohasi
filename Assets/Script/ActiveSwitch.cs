using UnityEngine;

public class ActiveSwitch : MonoBehaviour
{
    public GameObject observedObject; // 監視対象のオブジェクト
    public GameObject[] linkedObjects; // 切り替える対象のオブジェクトの配列

    private bool previousState; // 監視対象のオブジェクトの前回のアクティブ状態

    void Start()
    {
        if (observedObject == null)
        {
            Debug.LogError("Observed object is not assigned!");
            enabled = false; // スクリプトを無効化してエラーを防ぐ
            return;
        }

        // 初期状態を保存
        previousState = observedObject.activeSelf;
    }

    void Update()
    {
        if (observedObject == null) return;

        // 監視対象のオブジェクトのアクティブ状態が変化したかをチェック
        if (observedObject.activeSelf != previousState)
        {
            ToggleLinkedObjects();
            previousState = observedObject.activeSelf; // 前回の状態を更新
        }
    }

    private void ToggleLinkedObjects()
    {
        bool newState = observedObject.activeSelf;

        // リンクされたオブジェクトのアクティブ状態を切り替える
        foreach (var obj in linkedObjects)
        {
            if (obj != null)
            {
                obj.SetActive(newState);
            }
        }
    }
}
