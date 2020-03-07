using System;
using Photon.Pun;
using UnityEngine;

namespace MyGame
{
    public class PlayerInputController : MonoBehaviour
    {
        private PhotonView _photonView;
        
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public float DeltaMouseX { get; private set; }
        public float Jump { get; private set; }
        
        public event Action Fire;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void HandlePlayerInput()
        {
            if (_photonView.IsMine || GameManager.IsOfflineMode)
            {
                Horizontal = Input.GetAxis("Horizontal");
                Vertical = Input.GetAxis("Vertical");
                DeltaMouseX = Input.GetAxis("Mouse X");
                Jump = Input.GetAxis("Jump");
                
                if (Input.GetButtonDown("Fire1"))
                {
                    Fire?.Invoke();
                }
            }
        }

        private void Update()
        {
            HandlePlayerInput();
        }
    }
}
