using MyGame;
using Photon.Pun;
using UnityEngine;

public class PlayerRevivalHandler : MonoBehaviour
{
    private PlayerRevivalUIHandler _playerRevivalUiHandler;
    private MyNetworkPlayer _localNetworkPlayerInstance;

    private void Awake()
    {
        _playerRevivalUiHandler = GetComponent<PlayerRevivalUIHandler>();
        _localNetworkPlayerInstance = MyNetworkPlayer.LocalNetworkPlayerInstance;
    }

    private void OnEnable()
    {
        _playerRevivalUiHandler.CustomPlayerReborn += Reborn;
    }
    
    private void OnDisable()
    {
        _playerRevivalUiHandler.CustomPlayerReborn -= Reborn;
    }

    private void Reborn()
    {
        _localNetworkPlayerInstance.NetworkInitialize();
    }
}