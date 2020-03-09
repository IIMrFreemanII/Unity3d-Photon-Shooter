using MyGame;
using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Weapon weaponPrefab = null;
    private ProjectileLauncher _projectileLauncher;
    private NetworkPlayerInputController _networkPlayerInputController;
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _projectileLauncher = GetComponent<ProjectileLauncher>();
        _networkPlayerInputController = transform.parent.parent.parent.parent.GetComponent<NetworkPlayerInputController>();
    }

    private void OnEnable()
    {
        _networkPlayerInputController.Fire += NetworkFire;
    }
    
    private void OnDisable()
    {
        _networkPlayerInputController.Fire -= NetworkFire;
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
