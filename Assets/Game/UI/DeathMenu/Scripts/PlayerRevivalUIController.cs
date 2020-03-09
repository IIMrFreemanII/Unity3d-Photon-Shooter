using System;
using System.Collections;
using MyGame;
using UnityEngine;

public class PlayerRevivalUIController : MonoBehaviour
{
    [SerializeField] private DeathMessageContainer deathMessageContainer = null;
    
    private TimeToRevivalText _timeToRevivalText;
    private DeathMessageCard _deathMessageCard;
    private RevivalInfoCard _revivalInfoCard;
    private DeathMessageCanvas _deathMessageCanvas;
    private RevivalButton _revivalButton;
    
    private MyNetworkPlayer _localNetworkPlayerInstance;
    
    public float timeToRevival = 5f;
    public float currentTimeToRevival;

    private bool _isCountdown = false;

    public event Action CustomPlayerReborn;
    
    private void Awake()
    {
        currentTimeToRevival = timeToRevival;
        _localNetworkPlayerInstance = MyNetworkPlayer.LocalMyNetworkPlayerInstance;
    }

    private void Start()
    {
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
            _revivalButton.button.onClick.AddListener(Reborn);
        }));
    }
    
    private void OnDisable()
    {
        _revivalButton.button.onClick.RemoveListener(Reborn);
        _localNetworkPlayerInstance.OnDie -= StartCountdown;
    }

    private void HandleTimer()
    {
        if (currentTimeToRevival <= 0) return;
        
        if (currentTimeToRevival > 0)
        {
            currentTimeToRevival -= Time.deltaTime;
            _timeToRevivalText.SetText($"{Mathf.RoundToInt(currentTimeToRevival)}s");
        }

        if (currentTimeToRevival <= 0)
        {
            currentTimeToRevival = 0;
            ShowRevivalButton();
        }
    }

    private void InitialSetUp()
    {
        _timeToRevivalText = deathMessageContainer.timeToRevivalText;
        _deathMessageCard = deathMessageContainer.deathMessageCard;
        _revivalInfoCard = deathMessageContainer.revivalInfoCard;
        _deathMessageCanvas = deathMessageContainer.deathMessageCanvas;
        _revivalButton = deathMessageContainer.revivalButton;
        _revivalButton.Initialize();
    }

    private void ShowRevivalButton()
    {
        _isCountdown = false;
        _deathMessageCard.gameObject.SetActive(false);
        _revivalInfoCard.gameObject.SetActive(true);
    }

    [ContextMenu("Start countdown")]
    public void StartCountdown()
    {
        _isCountdown = true;
        
        timeToRevival = 5f;
        currentTimeToRevival = timeToRevival;

        _deathMessageCanvas.gameObject.SetActive(true);
        _deathMessageCard.gameObject.SetActive(true);
        _revivalInfoCard.gameObject.SetActive(false);
    }

    private void Reborn()
    {
        _deathMessageCanvas.gameObject.SetActive(false);
        _deathMessageCard.gameObject.SetActive(true);
        _revivalInfoCard.gameObject.SetActive(false);

        print("Reborn");

        CustomPlayerReborn?.Invoke();
    }

    private IEnumerator InvokeWithDelay(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}