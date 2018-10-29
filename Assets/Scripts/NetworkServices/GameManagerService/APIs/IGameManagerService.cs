using System;
using UnityEngine.Networking.NetworkSystem;

public enum Mode
{
    IA,
    Local,
    Online
}

public interface IGameManagerService
{
    event Action<int[], int> NewGameReceived;
    event Action<int, int, int?> CardUpdate;
    event Action<GameResult> GameFinished;
    event Action<bool> Splashed;
    
    void Initialize();
    void Start(Mode mode);
    void PlayThisCard(int positionCardSelected);
    void HumanSplash();
}
