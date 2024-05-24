using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void cange_button()
    {
        SceneManager.LoadScene("SampleScene");
    }
}