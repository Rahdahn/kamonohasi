public static class GameData
{
    // �����̎�ނ��Ƃ̕ߊl��
    public static int D1Count { get; set; } = 2;
    public static int D2Count { get; set; } = 2;
    public static int D3Count { get; set; } = 2;
    public static int D4Count { get; set; } = 2;
    public static int D5Count { get; set; } = 2;

    // �ߊl�������Z�b�g���郁�\�b�h
    public static void ResetCounts()
    {
        D1Count = 2;
        D2Count = 2;
        D3Count = 2;
        D4Count = 2;
        D5Count = 2;
    }
}