using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData = null;
    
    [SerializeField]
    private ProjectileSpawnPosition projectileSpawnPosition = null;

    private void Awake()
    {
        projectileSpawnPosition = GetComponentInChildren<ProjectileSpawnPosition>();
    }

    public void Launch(Player owner)
    {
        GameObject bullet = Instantiate(weaponData.bulletPrefab, projectileSpawnPosition.transform.position, transform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Launch(owner, weaponData.bulletSpeed, weaponData.bulletDamage);
    }
}