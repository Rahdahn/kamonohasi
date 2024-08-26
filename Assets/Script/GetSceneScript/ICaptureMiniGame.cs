namespace MiniGames
{
    public interface ICaptureMiniGame
    {
        void StartMiniGame(System.Action<bool> onComplete);
    }
}
