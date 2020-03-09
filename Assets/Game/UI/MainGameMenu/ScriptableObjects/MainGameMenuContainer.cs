using UnityEngine;

[CreateAssetMenu]
public class MainGameMenuContainer : ScriptableObject
{
    public MainGameMenuCanvas mainGameMenuCanvas;
    
    public ContinueGameButton continueGameButton;
    public SettingsButton settingsButton;
    public LeaveRoomButton leaveRoomButton;
}
