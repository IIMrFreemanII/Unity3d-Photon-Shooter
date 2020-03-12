using System;
using MyGame;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PhotonView))]
public class NetworkPlayerController : MonoBehaviour, INetworkTarget, IPunObservable
{
    private PhotonView _photonView;
    private Rigidbody _rb;
    
    [SerializeField] private PlayerData playerData = null;
    
    [SerializeField] private float health = 0;
    public float Health
    {
        get => health;
        set
        {
            if (_photonView.IsMine)
            {
                playerData.CurrentHealth = value;
            }

            health = value;
        }
    }
    
    public event Action OnDie;
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Health);
        } 
        else
        {
            Health = (float) stream.ReceiveNext();
        }
    }

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _rb = GetComponent<Rigidbody>();
    }
    
    private void OnEnable()
    {
        Health = playerData.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        print($"{_photonView.Owner.NickName} got {damage} damage.");
        
        Health -= damage;
        if (Health <= 0)
        {
            NetworkDie();
        }
        
        print($"{_photonView.Owner.NickName} has: {Health} health.");

    }

    [ContextMenu("Die")]
    private void CustomDie()
    {
        NetworkDie();
    }

    public void NetworkDie()
    {
        if (_photonView.IsMine)
        {
            _photonView.RPC("Die", RpcTarget.All);
        }
    }
    [PunRPC]
    private void Die()
    {
        gameObject.SetActive(false);
        Health = 0f;
        OnDie?.Invoke();
    }

    public void NetworkInitialize()
    {
        if (_photonView.IsMine)
        {
            _photonView.RPC("Initialize", RpcTarget.All);
        }
    }
    [PunRPC]
    private void Initialize()
    {
        transform.position = PlayerManager.DefaultSpawnPosition;
        transform.rotation = PlayerManager.DefaultSpawnRotation;
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(true);
            
        Health = playerData.MaxHealth;
    }
}
