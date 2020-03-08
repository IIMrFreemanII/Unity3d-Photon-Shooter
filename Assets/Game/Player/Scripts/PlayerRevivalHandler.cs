using MyGame;
using Photon.Pun;
using UnityEngine;

public class PlayerRevivalHandler : MonoBehaviour
{
    private PlayerRevivalUIHandler _playerRevivalUiHandler;
    private MyPlayer _localPlayerInstance;

    private void Awake()
    {
        _playerRevivalUiHandler = GetComponent<PlayerRevivalUIHandler>();
        _localPlayerInstance = MyPlayer.LocalPlayerInstance;
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
        _localPlayerInstance.NetworkInitialize();
    }
}