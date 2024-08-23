using UnityEngine;

public class LogOnDisable : MonoBehaviour
{
    void OnDisable()
    {
        // このスクリプトがアタッチされたオブジェクトが非アクティブになった時に呼ばれる
        Debug.Log($"{gameObject.name} オブジェクトが非アクティブになりました (スクリプト: {this.GetType().Name})");
    }
}
