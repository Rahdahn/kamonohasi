using UnityEngine;
using TMPro;

public class AnimalCounter : MonoBehaviour
{
    public TextMeshProUGUI d1CountText;
    public TextMeshProUGUI d2CountText;
    public TextMeshProUGUI d3CountText;
    public TextMeshProUGUI d4CountText;
    public TextMeshProUGUI d5CountText;

    private void Start()
    {
        LoadAnimalCounts();
        UpdateAnimalCountDisplay();
    }

    public void IncrementCount(AnimalType type)
    {
        int count = PlayerPrefs.GetInt(type.ToString(), 0);
        count++;
        PlayerPrefs.SetInt(type.ToString(), count);
        PlayerPrefs.Save();
        UpdateAnimalCountDisplay();
    }

    private void UpdateAnimalCountDisplay()
    {
        d1CountText.text = $"X {PlayerPrefs.GetInt(AnimalType.D1.ToString(), 0)}";
        d2CountText.text = $"X {PlayerPrefs.GetInt(AnimalType.D2.ToString(), 0)}";
        d3CountText.text = $"X {PlayerPrefs.GetInt(AnimalType.D3.ToString(), 0)}";
        d4CountText.text = $"X {PlayerPrefs.GetInt(AnimalType.D4.ToString(), 0)}";
        d5CountText.text = $"X {PlayerPrefs.GetInt(AnimalType.D5.ToString(), 0)}";
    }

    private void LoadAnimalCounts()
    {
        // このメソッドは実際には PlayerPrefs に保存されたデータをロードする
        // 特に必要ない場合は省略可能
    }
}
