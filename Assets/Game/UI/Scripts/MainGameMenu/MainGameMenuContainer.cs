using UnityEngine;

public class MainGameMenuContainer : MonoBehaviour
{
    public MainGameMenuCanvas mainGameMenuCanvas;
    
    public ContinueGameButton continueGameButton;
    public SettingsButton settingsButton;
    public LeaveRoomButton leaveRoomButton;

    private void Awake()
    {
        InitializeMainGameMenuContainer();
    }

    private void InitializeMainGameMenuContainer()
    {
        mainGameMenuCanvas = GetComponentInChildren<MainGameMenuCanvas>(true);
        continueGameButton = GetComponentInChildren<ContinueGameButton>(true);
        settingsButton = GetComponentInChildren<SettingsButton>(true);
        leaveRoomButton = GetComponentInChildren<LeaveRoomButton>(true);
    }
    
}
