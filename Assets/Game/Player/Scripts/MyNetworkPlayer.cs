using System;
using Photon.Pun;
using UnityEngine;

namespace MyGame
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PhotonView))]
    public class MyNetworkPlayer : MonoBehaviour, ITarget
    {
        public static MyNetworkPlayer LocalMyNetworkPlayerInstance;
        
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

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _rb = GetComponent<Rigidbody>();

            if (_photonView.IsMine)
            {
                LocalMyNetworkPlayerInstance = this;
            }
        }

        private void Start()
        {
            CameraHandler cameraHandler = GetComponent<CameraHandler>();

            if (cameraHandler != null)
            {
                if (_photonView.IsMine)
                {
                    cameraHandler.OnStartFollowing();
                }
            }
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

        private void NetworkDie()
        {
            if (_photonView.IsMine)
            {
                _photonView.RPC("Die", RpcTarget.All);
            }
        }

        [PunRPC]
        public void Die()
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
            transform.position = new Vector3(0f, 5f, 0f);
            transform.rotation = Quaternion.identity;
            _rb.velocity = Vector3.zero;
            gameObject.SetActive(true);
            
            Health = playerData.MaxHealth;
        }
    }
}