using UnityEngine;

[CreateAssetMenu]
public class DeathMessageContainer : ScriptableObject
{
    public DeathMessageCanvas deathMessageCanvas;
    public DeathMessagePanel deathMessagePanel;
    public DeathMessageCard deathMessageCard;
    public TimeToRevivalText timeToRevivalText;
    public RevivalInfoCard revivalInfoCard;
    public RevivalButton revivalButton;
}