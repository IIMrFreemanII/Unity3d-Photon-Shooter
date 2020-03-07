using MyGame;
using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    private Bullet bulletPrefab = null;
    [SerializeField]
    private ProjectileSpawnPosition projectileSpawnPosition = null;
    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    private float damage = 10f;
    

    private Weapon _weapon = null;
    
    private void Awake()
    {
        projectileSpawnPosition = GetComponentInChildren<ProjectileSpawnPosition>();
        bulletPrefab = App.Instance.WeaponsData.bulletPrefab;
        _weapon = GetComponent<Weapon>();
    }

    public void Launch(Player owner)
    {
        Bullet bullet = Instantiate(bulletPrefab, projectileSpawnPosition.transform.position, transform.rotation);
        bullet.Launch(owner, speed, damage);
    }
}