namespace Mobility.Movement {
    public delegate void OnGameEnd(bool won);

    public interface IGameState {
        bool isGameOver { get; }

        bool won { get; }

        OnGameEnd onGameEndListener { get; set; }

        void EndGame(bool won);
    }
}
