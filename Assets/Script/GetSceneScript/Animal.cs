using UnityEngine;

public enum AnimalType
{
    D1,
    D2,
    D3,
    D4,
    D5
}

public class Animal : MonoBehaviour
{
    public AnimalType type;
    // 他に必要な動物の情報や振る舞いを追加することができます
}
