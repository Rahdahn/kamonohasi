using UnityEngine;

public class Hdeanimal : MonoBehaviour
{
    public GameObject targetObject; // 対象となるオブジェクト
    public string scriptTypeName; // 無効にするスクリプトの型名

    private MonoBehaviour scriptToDisable; // 無効にするスクリプト

    private void Start()
    {
        // 指定された型名のコンポーネントを検索
        scriptToDisable = targetObject.GetComponent(scriptTypeName) as MonoBehaviour;

        if (scriptToDisable == null)
        {
            Debug.LogError("Script not found on target object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            // アタッチされたスクリプトを無効にする
            if (scriptToDisable != null)
            {
                scriptToDisable.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            // アタッチされたスクリプトを再度有効にする
            if (scriptToDisable != null)
            {
                scriptToDisable.enabled = true;
            }
        }
    }
}
