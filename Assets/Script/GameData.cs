public static class GameData
{
    // �����̎�ނ��Ƃ̕ߊl��
    public static int D1Count { get; set; } = 0;
    public static int D2Count { get; set; } = 1;
    public static int D3Count { get; set; } = 2;
    public static int D4Count { get; set; } = 3;
    public static int D5Count { get; set; } = 4;

    // �ߊl�������Z�b�g���郁�\�b�h
    public static void ResetCounts()
    {
        D1Count = 0;
        D2Count = 0;
        D3Count = 0;
        D4Count = 0;
        D5Count = 0;
    }
}
