using MyGame;
using UnityEngine;

public class PlayerRevivalController : MonoBehaviour
{
    private PlayerRevivalUIController _playerRevivalUiController;
    private MyNetworkPlayer _localNetworkPlayerInstance;
    private NetworkPlayerController _networkPlayerController;

    private void Awake()
    {
        _playerRevivalUiController = GetComponent<PlayerRevivalUIController>();
        _localNetworkPlayerInstance = MyNetworkPlayer.MyLocalNetworkPlayerInstance;
        _networkPlayerController = _localNetworkPlayerInstance.GetComponent<NetworkPlayerController>();
    }

    private void OnEnable()
    {
        _playerRevivalUiController.CustomPlayerReborn += Reborn;
    }
    
    private void OnDisable()
    {
        _playerRevivalUiController.CustomPlayerReborn -= Reborn;
    }

    private void Reborn()
    {
        _networkPlayerController.NetworkInitialize();
    }
}