using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GaugeController : MonoBehaviour
{
    public Slider slider;
    public Button button;
    public float minSpeed = 0.5f;
    public float maxSpeed = 3.0f;
    public float successThreshold = 0.8f;
    private bool isFilling = true;
    private bool isIncreasing = true;

    public MovementController[] movementControllers;
    public ObjectActivator[] objectActivators;

    private ActiveManager activeManager;

    public TextMeshProUGUI resultText;

    // アクティブ状態を切り替える対象のオブジェクト（複数）
    public GameObject[] objectsToToggle;

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);

        activeManager = FindObjectOfType<ActiveManager>();
        if (activeManager == null)
        {
            Debug.LogError("ActiveManager not found in the scene!");
        }

        gameObject.SetActive(false);

        if (resultText != null)
        {
            resultText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (slider.gameObject.activeInHierarchy)
        {
            if (isFilling)
            {
                float speed = Mathf.Lerp(minSpeed, maxSpeed, slider.value);

                if (isIncreasing)
                {
                    slider.value += speed * Time.deltaTime;
                    if (slider.value >= slider.maxValue)
                    {
                        isIncreasing = false;
                    }
                }
                else
                {
                    slider.value -= speed * Time.deltaTime;
                    if (slider.value <= slider.minValue)
                    {
                        isIncreasing = true;
                    }
                }
            }
        }
    }

    void OnButtonClick()
    {
        if (!isFilling) return;

        isFilling = false;

        if (slider.value >= successThreshold)
        {
            Debug.Log("Success!");
            AwardItem();

            foreach (var movementController in movementControllers)
            {
                if (movementController != null)
                {
                    movementController.ResumeMovement();
                }
            }

            DisplayResult("Success!");
        }
        else
        {
            Debug.Log("Failure.");
            DisplayResult("Failure.");

            foreach (var movementController in movementControllers)
            {
                if (movementController != null)
                {
                    movementController.ResumeMovement();
                }
            }
        }

        slider.gameObject.SetActive(false);
        button.gameObject.SetActive(false);

        foreach (var objectActivator in objectActivators)
        {
            if (objectActivator != null)
            {
                objectActivator.Deactivate();
            }
        }

        ToggleObjectsActiveState();

        if (activeManager != null)
        {
            activeManager.RegisterInactiveObject(this);
        }
        gameObject.SetActive(false);
    }

    void AwardItem()
    {
        Debug.Log("Item awarded!");
    }

    void OnEnable()
    {
        slider.value = slider.minValue;
        isFilling = true;
        isIncreasing = true;
    }

    public void ActivateSliderAndButton()
    {
        slider.gameObject.SetActive(true);
        button.gameObject.SetActive(true);

        ResetSlider();
    }

    public void Reactivate()
    {
        gameObject.SetActive(true);
        ActivateSliderAndButton();
    }

    private void ResetSlider()
    {
        slider.value = slider.minValue;
        isFilling = true;
        isIncreasing = true;
    }

    private void DisplayResult(string message)
    {
        if (resultText != null)
        {
            resultText.text = message;
            resultText.gameObject.SetActive(true);
            Invoke("HideResult", 2f);
        }
    }

    private void HideResult()
    {
        if (resultText != null)
        {
            resultText.gameObject.SetActive(false);
        }
    }

    private void ToggleObjectsActiveState()
    {
        foreach (var obj in objectsToToggle)
        {
            if (obj != null)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }

    public bool IsFilling()
    {
        return isFilling;
    }

    public void StartFilling()
    {
        isFilling = true;
    }

    public void StopFilling()
    {
        isFilling = false;
    }
}
