using Photon.Pun;
using Photon.Realtime;

public class Weapon : MonoBehaviourPun
{
    private ProjectileLauncher _projectileLauncher;

    private void Awake()
    {
        _projectileLauncher = GetComponent<ProjectileLauncher>();

        // if (!photonView.IsMine) return;
    }

    public void Fire(Player owner)
    {
        _projectileLauncher.Launch(owner);
    }

    // public void OnPhotonInstantiate(PhotonMessageInfo info)
    // {
    //     if (!photonView.IsMine)
    //     {
    //         print($"{info.Sender.NickName}: {info.photonView.ViewID}");
    //     }
    // }
}
