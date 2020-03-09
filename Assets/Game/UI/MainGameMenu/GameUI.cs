using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;
    public static bool IsPause = false;

    public MainGameMenuContainer mainGameMenuContainer;
    public DeathMessageContainer deathMessageContainer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        InitializeMainGameMenuContainer();
        InitializeYouAreDeadCanvas();
    }

    private void InitializeMainGameMenuContainer()
    {
        mainGameMenuContainer.mainGameMenuCanvas = GetComponentInChildren<MainGameMenuCanvas>(true);
        mainGameMenuContainer.continueGameButton = GetComponentInChildren<ContinueGameButton>(true);
        mainGameMenuContainer.settingsButton = GetComponentInChildren<SettingsButton>(true);
        mainGameMenuContainer.leaveRoomButton = GetComponentInChildren<LeaveRoomButton>(true);
    }

    private void InitializeYouAreDeadCanvas()
    {
        deathMessageContainer.deathMessageCanvas = GetComponentInChildren<DeathMessageCanvas>(true);
        deathMessageContainer.deathMessagePanel = GetComponentInChildren<DeathMessagePanel>(true);
        deathMessageContainer.deathMessageCard = GetComponentInChildren<DeathMessageCard>(true);
        deathMessageContainer.timeToRevivalText = GetComponentInChildren<TimeToRevivalText>(true);
        deathMessageContainer.revivalInfoCard = GetComponentInChildren<RevivalInfoCard>(true);
        deathMessageContainer.revivalButton = GetComponentInChildren<RevivalButton>(true);
    }
}