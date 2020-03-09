using System;
using System.Collections;
using MyGame;
using UnityEngine;

public class PlayerRevivalUIHandler : MonoBehaviour
{
    [SerializeField]
    private TimeToRevivalText timeToRevivalText = null;
    [SerializeField]
    private DeathMessageCard deathMessageCard = null;
    [SerializeField]
    private RevivalInfoCard revivalInfoCard = null;
    [SerializeField]
    private DeathMessageCanvas deathMessageCanvas = null;
    private DeathMessageContainer _deathMessageContainer;
    public RevivalButton revivalButton;
    private MyNetworkPlayer _localNetworkPlayerInstance;
    
    public float timeToRevival = 5f;
    public float currentTimeToRevival;

    private bool _isCountdown = false;

    public event Action CustomPlayerReborn;
    
    private void Awake()
    {
        currentTimeToRevival = timeToRevival;
        _localNetworkPlayerInstance = MyNetworkPlayer.LocalNetworkPlayerInstance;
    }

    private void Start()
    {
        _deathMessageContainer = GameUI.Instance.deathMessageContainer;
        
        InitialSetUp();
    }

    private void Update()
    {
        if (!_isCountdown) return;
        
        HandleTimer();
    }

    private void OnEnable()
    {
        _localNetworkPlayerInstance.OnDie += StartCountdown;
        
        StartCoroutine(InvokeWithDelay(0f, () =>
        {
            revivalButton.revivalButton.onClick.AddListener(Reborn);
        }));
    }
    
    private void OnDisable()
    {
        revivalButton.revivalButton.onClick.RemoveListener(Reborn);
        _localNetworkPlayerInstance.OnDie -= StartCountdown;
    }

    private void HandleTimer()
    {
        if (currentTimeToRevival <= 0) return;
        
        if (currentTimeToRevival > 0)
        {
            currentTimeToRevival -= Time.deltaTime;
            timeToRevivalText.SetText($"{Mathf.RoundToInt(currentTimeToRevival)}s");
        }

        if (currentTimeToRevival <= 0)
        {
            currentTimeToRevival = 0;
            ShowRevivalButton();
        }
    }

    private void InitialSetUp()
    {
        timeToRevivalText = _deathMessageContainer.timeToRevivalText;
        deathMessageCard = _deathMessageContainer.deathMessageCard;
        revivalInfoCard = _deathMessageContainer.revivalInfoCard;
        deathMessageCanvas = _deathMessageContainer.deathMessageCanvas;
        revivalButton = _deathMessageContainer.revivalButton;
        revivalButton.Initialize();
    }

    private void ShowRevivalButton()
    {
        _isCountdown = false;
        deathMessageCard.gameObject.SetActive(false);
        revivalInfoCard.gameObject.SetActive(true);
    }

    [ContextMenu("Start countdown")]
    public void StartCountdown()
    {
        _isCountdown = true;
        
        timeToRevival = 5f;
        currentTimeToRevival = timeToRevival;

        deathMessageCanvas.gameObject.SetActive(true);
        deathMessageCard.gameObject.SetActive(true);
        revivalInfoCard.gameObject.SetActive(false);
    }

    private void Reborn()
    {
        deathMessageCanvas.gameObject.SetActive(false);
        deathMessageCard.gameObject.SetActive(true);
        revivalInfoCard.gameObject.SetActive(false);

        print("Reborn");

        CustomPlayerReborn?.Invoke();
    }

    private IEnumerator InvokeWithDelay(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}