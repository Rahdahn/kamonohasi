using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Add this line to include SceneManager

public class SlideMoveEnd : MonoBehaviour
{
    [SerializeField] Slider currentSlider;
    private bool isClicked;
    private bool maxValue;

    void Start()
    {
        UnityEngine.Debug.Log("start");
        currentSlider.value = 0;
        maxValue = false;
        isClicked = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isClicked == false)
        {
            UnityEngine.Debug.Log("stop");
            isClicked = true;
        }
        else if (Input.GetMouseButtonDown(0) && isClicked == true)
        {
            UnityEngine.Debug.Log("start");
            isClicked = false;
        }
        if (isClicked)
        {
            if (currentSlider.value >= 4.5 && currentSlider.value <= 5.5)
            {
                UnityEngine.Debug.Log("Great!!");
                currentSlider.gameObject.SetActive(false); // Deactivate the current slider
                SceneManager.LoadScene("ResultScene");  // Load the ResultScene
            }
            return;
        }

        if (currentSlider.value == 10)
        {
            maxValue = true;
        }
        if (currentSlider.value == 0)
        {
            maxValue = false;
        }

        if (maxValue == true)
        {
            currentSlider.value -= 0.03f;
        }

        if (maxValue == false)
        {
            currentSlider.value += 0.03f;
        }
    }
}
