using System.Collections;
using UnityEngine;

public class HandleParticle : MonoBehaviour
{
    [SerializeField] private new  ParticleSystem particleSystem = null;
    [SerializeField] private float timeToDie;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        particleSystem.Play();
        StartCoroutine(DieWithDelay(timeToDie));
    }

    IEnumerator DieWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Die();
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
