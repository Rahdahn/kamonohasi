using UnityEngine;
using System.Collections;

public class Gauge : MonoBehaviour
{
    public CircularGauge gauge;
    public float increaseAmount = 0.1f;
    public float increaseInterval = 0.5f;
    private bool isIncreasing = true;

    void Start()
    {
        if (gauge == null)
        {
            Debug.LogError("CircularGauge is not assigned!");
            return;
        }

        StartCoroutine(IncreaseGaugeOverTime());
    }

    IEnumerator IncreaseGaugeOverTime()
    {
        while (isIncreasing)
        {
            gauge.IncreaseGauge(increaseAmount);
            if (gauge.CurrentValue >= gauge.maxValue)
            {
                isIncreasing = false;
            }
            yield return new WaitForSeconds(increaseInterval);
        }
    }
}
