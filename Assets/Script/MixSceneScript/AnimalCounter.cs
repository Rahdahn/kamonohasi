using UnityEngine;
using TMPro;  // TextMeshPro�p

public class AnimalCounter : MonoBehaviour
{
    public TextMeshProUGUI d1CountText;
    public TextMeshProUGUI d2CountText;
    public TextMeshProUGUI d3CountText;
    public TextMeshProUGUI d4CountText;
    public TextMeshProUGUI d5CountText;

    void Start()
    {
        UpdateAnimalCountDisplay();
    }

    private void UpdateAnimalCountDisplay()
    {
        // �e��ނ̃J�E���g��TextMeshPro�ɔ��f
        d1CountText.text = $"X {GameData.D1Count}";
        d2CountText.text = $"X {GameData.D2Count}";
        d3CountText.text = $"X {GameData.D3Count}";
        d4CountText.text = $"X {GameData.D4Count}";
        d5CountText.text = $"X {GameData.D5Count}";
    }
}
