using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CangeScene : MonoBehaviour
{
    public void cange_button()
    {
        SceneManager.LoadScene("GetScene");
    }
}