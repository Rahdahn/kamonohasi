using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedGameObject : MonoBehaviour
{
    GameObject clickedGameObject; // クリックされたゲームオブジェクトを代入する変数
    bool isInsideCheckpoint = false; // チェックポイントにいるかどうかを示すフラグ

    // チェックポイントに入ったときに呼び出される関数
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            isInsideCheckpoint = true;
            Debug.Log("Entered checkpoint: " + other.gameObject.name); // チェックポイントに入ったときのデバッグログ
        }
    }

    // チェックポイントから出たときに呼び出される関数
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            isInsideCheckpoint = false;
            Debug.Log("Exited checkpoint: " + other.gameObject.name); // チェックポイントから出たときのデバッグログ
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
                Debug.Log(clickedGameObject.name); // ゲームオブジェクトの名前を出力
                Destroy(clickedGameObject); // ゲームオブジェクトを破壊
            }
        }
    }
}
