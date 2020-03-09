using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;

    public float bulletSpeed;
    public float bulletDamage;
}
