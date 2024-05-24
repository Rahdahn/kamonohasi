using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 2.0f; // カメラの移動速度
    public float stopY = 0.0f; // カメラの停止するY座標

    private bool sceneStarted = false; // シーンが始まったかどうかのフラグ

    void Start()
    {
        sceneStarted = true; // シーンが始まったことを示すフラグを立てる
    }

    void Update()
    {
        if (sceneStarted)
        {
            // カメラを下に動かす
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            // カメラのY座標が停止位置を超えたら停止する
            if (transform.position.y <= stopY)
            {
                // カメラの位置を直接停止位置に設定
                transform.position = new Vector3(transform.position.x, stopY, transform.position.z);
                sceneStarted = false; // シーンが停止したことを示すフラグを立てる
            }
        }

        // シーンが始まっているときだけマウスクリックを無視する
        if (Input.GetMouseButtonDown(0) && sceneStarted)
        {
            return;
        }
    }
}
