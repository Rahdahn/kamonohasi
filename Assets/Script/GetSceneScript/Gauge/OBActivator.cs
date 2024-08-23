using UnityEngine;

public class OBActivator : MonoBehaviour
{
    public void Deactivate()
    {
        // ここにオブジェクトを非アクティブ化する処理を追加
        gameObject.SetActive(false);
        Debug.Log(gameObject.name + " deactivated!");
    }
}
