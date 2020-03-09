using System;
using Photon.Pun;
using UnityEngine;

namespace MyGame
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PhotonView))]
    public class MyNetworkPlayer : MonoBehaviour, ITarget
    {
        public MyNetworkPlayer networkPlayerPrefab = null;
        public static MyNetworkPlayer LocalNetworkPlayerInstance;
        private PhotonView _photonView;
        private Rigidbody _rb;
        
        [SerializeField] private float health = 100f;
        public float Health
        {
            get => health;
            set => health = value;
        }
        
        public event Action OnDie;

        private void Awake()
        {
            networkPlayerPrefab = this;
            _photonView = GetComponent<PhotonView>();
            _rb = GetComponent<Rigidbody>();

            if (_photonView.IsMine)
            {
                LocalNetworkPlayerInstance = this;
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
            Health = 100f;
        }
    }
}