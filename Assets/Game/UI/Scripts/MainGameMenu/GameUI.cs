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

        mainGameMenuContainer = GetComponentInChildren<MainGameMenuContainer>(true);
        deathMessageContainer = GetComponentInChildren<DeathMessageContainer>(true);
    }
}