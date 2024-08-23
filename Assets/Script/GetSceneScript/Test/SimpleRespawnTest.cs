using UnityEngine;
using System.Collections;

public class SimpleRespawnTest : MonoBehaviour
{
    public GameObject testObject; // テストするオブジェクト
    public float respawnDelay = 3.0f; // リスポーンまでの遅延

    void Start()
    {
        if (testObject != null)
        {
            StartCoroutine(RespawnTest());
        }
        else
        {
            Debug.LogError("Test object is not assigned.");
        }
    }

    IEnumerator RespawnTest()
    {
        Debug.Log("Starting test respawn.");

        testObject.SetActive(false);
        Debug.Log("Test object deactivated.");

        // リスポーン遅延の待機
        yield return new WaitForSeconds(respawnDelay);

        Debug.Log("Wait time over, reactivating test object.");
        testObject.SetActive(true);
        Debug.Log("Test object reactivated.");
    }
}
