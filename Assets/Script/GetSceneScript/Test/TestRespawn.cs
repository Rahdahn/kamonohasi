using UnityEngine;
using System.Collections;

public class TestRespawn : MonoBehaviour
{
    public GameObject testObject;
    public float respawnDelay = 3.0f;

    void Start()
    {
        if (testObject != null)
        {
            StartCoroutine(RespawnTest());
        }
    }

    IEnumerator RespawnTest()
    {
        Debug.Log("Starting test respawn.");
        testObject.SetActive(false);
        Debug.Log("Test object deactivated.");
        yield return new WaitForSeconds(respawnDelay);
        testObject.SetActive(true);
        Debug.Log("Test object reactivated.");
    }
}
