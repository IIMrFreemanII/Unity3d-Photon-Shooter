using System.Collections;
using Extensions;
using MyGame;
using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, ICanDamage
{
    public Bullet bulletPrefab;
    [SerializeField] private ParticleSystem bulletHitEffect = null;
    private Rigidbody _rb;
    public Player Owner { get; private set; }
    private Vector3 _lastPosition;

    [SerializeField]
    private float timeToDie = 0f;

    public float Damage { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Vector3 currentPosition = transform.position + (transform.forward * 0.2f);
        if (Physics.Linecast(_lastPosition, currentPosition, out RaycastHit hit))
        {
            hit.collider.gameObject.HandleComponent<INetworkTarget>(target => ApplyDamage(target, Damage));
            Instantiate(bulletHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Die();
        }
            
        _lastPosition = transform.position;
    }

    public void Launch(Player owner, float speed, float damage)
    {
        _lastPosition = transform.position;
        Owner = owner;
        Damage = damage;
        
        _rb.AddForce(transform.forward * speed, ForceMode.Impulse);
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
