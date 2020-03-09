using Photon.Realtime;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private ProjectileLauncher _projectileLauncher;

    private void Awake()
    {
        _projectileLauncher = GetComponent<ProjectileLauncher>();
    }

    public void Fire(Player owner)
    {
        _projectileLauncher.Launch(owner);
    }
}
