using MyGame;
using Photon.Pun;
using UnityEngine;

public class PlayerRevivalController : MonoBehaviour
{
    private PlayerRevivalUIController _playerRevivalUiController;
    private MyNetworkPlayer _localNetworkPlayerInstance;

    private void Awake()
    {
        _playerRevivalUiController = GetComponent<PlayerRevivalUIController>();
        _localNetworkPlayerInstance = MyNetworkPlayer.LocalMyNetworkPlayerInstance;
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
        _localNetworkPlayerInstance.NetworkInitialize();
    }
}