using UnityEngine;

public class DeathMessageContainer : MonoBehaviour
{
    public DeathMessageCanvas deathMessageCanvas;
    public DeathMessagePanel deathMessagePanel;
    public DeathMessageCard deathMessageCard;
    public TimeToRevivalText timeToRevivalText;
    public RevivalInfoCard revivalInfoCard;
    public RevivalButton revivalButton;

    private void Awake()
    {
        InitializeYouAreDeadCanvas();
    }

    private void InitializeYouAreDeadCanvas()
    {
        deathMessageCanvas = GetComponentInChildren<DeathMessageCanvas>(true);
        deathMessagePanel = GetComponentInChildren<DeathMessagePanel>(true);
        deathMessageCard = GetComponentInChildren<DeathMessageCard>(true);
        timeToRevivalText = GetComponentInChildren<TimeToRevivalText>(true);
        revivalInfoCard = GetComponentInChildren<RevivalInfoCard>(true);
        revivalButton = GetComponentInChildren<RevivalButton>(true);
    }
}