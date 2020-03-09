using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Weapon weaponPrefab = null;
    private ProjectileLauncher _projectileLauncher;
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _projectileLauncher = GetComponent<ProjectileLauncher>();
    }

    public void Fire()
    {
        _projectileLauncher.Launch(_photonView.Owner);
    }
}
