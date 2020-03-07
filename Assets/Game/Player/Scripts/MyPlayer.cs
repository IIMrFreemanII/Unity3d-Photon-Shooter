using Photon.Pun;
using UnityEngine;

namespace MyGame
{
    public class MyPlayer : MonoBehaviour, ITarget
    {
        public MyPlayer playerPrefab = null;
        public static MyPlayer LocalPlayerInstance;
        private PhotonView _photonView;
        
        [SerializeField] private float health = 100f;
        public float Health
        {
            get => health;
            set => health = value;
        }

        private void Awake()
        {
            playerPrefab = this;
            _photonView = GetComponent<PhotonView>();

            if (_photonView.IsMine)
            {
                LocalPlayerInstance = this;
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
                Die();
            }
        
            print($"{_photonView.Owner.NickName} has: {Health} health.");
        }

        public void Die()
        {
            if (_photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
