using System.Collections;
using Extensions;
using MyGame;
using Photon.Realtime;
using UnityEngine;

public class Bullet : MonoBehaviour, ICanDamage
{
    public Bullet bulletPrefab;
    [SerializeField] private ParticleSystem bulletHitEffect = null;
    public Player Owner { get; private set; }
    private Vector3 _lastPosition;

    [SerializeField] private float timeToDie = 0f;
    [SerializeField] private float bulletSpeed = 0f;

    public float Damage { get; set; }

    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * bulletSpeed));
        
        Vector3 currentPosition = transform.position + (transform.forward * 0.2f);
        if (Physics.Linecast(_lastPosition, currentPosition, out RaycastHit hit))
        {
            hit.collider.gameObject.HandleComponent<INetworkTarget>(target => ApplyDamage(target, Damage));
            Instantiate(bulletHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Die();
        }
            
        _lastPosition = transform.position;
    }

    public void Launch(Player owner, float speed, float damage, Vector3 spawnPosition)
    {
        bulletSpeed = speed;
        transform.position = spawnPosition;
        _lastPosition = spawnPosition;
        Owner = owner;
        Damage = damage;
        
        StartCoroutine(DieWithDelay(timeToDie));
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator DieWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Die();
    }
    
    public void ApplyDamage(INetworkTarget networkTarget, float damage)
    {
        networkTarget.TakeDamage(Damage);
    }
}
