using MyGame;
using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Weapon weaponPrefab = null;
    private ProjectileLauncher _projectileLauncher;
    private PlayerInputController _playerInputController;
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _projectileLauncher = GetComponent<ProjectileLauncher>();
        _playerInputController = transform.parent.parent.parent.GetComponent<PlayerInputController>();
    }

    private void OnEnable()
    {
        _playerInputController.Fire += NetworkFire;
    }
    
    private void OnDisable()
    {
        _playerInputController.Fire -= NetworkFire;
    }

    private void NetworkFire()
    {
        _photonView.RPC("Fire", RpcTarget.All);
    }

    [PunRPC]
    private void Fire()
    {
        _projectileLauncher.Launch(_photonView.Owner);
    }
}
