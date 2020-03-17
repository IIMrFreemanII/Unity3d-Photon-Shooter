using System;
using Photon.Pun;
using UnityEngine;

namespace MyGame
{
    [RequireComponent(typeof(PhotonView))]
    public class NetworkPlayerInputController : MonoBehaviour
    {
        private PhotonView _photonView;
        
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public float sensitivity = 200;
        
        private float mouseX;
        public float DeltaMouseX
        {
            get => mouseX;
            private set => mouseX = value;
        }

        private float mouseY;
        public float DeltaMouseY
        {
            get => mouseY;
            private set => mouseY = value;
        }
        
        public float Jump { get; private set; }
        
        public event Action Fire;
        public event Action SwitchToFpsOrTps;
        public event Action SwitchShoulder;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void HandlePlayerInput()
        {
            if (!_photonView.IsMine) return;
            
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            
            DeltaMouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
            DeltaMouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;
                
            Jump = Input.GetAxis("Jump");
                
            if (Input.GetButtonDown("Fire1"))
            {
                Fire?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchToFpsOrTps?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchShoulder?.Invoke();
            }
        }

        private void Update()
        {
            CheckForPause(HandlePlayerInput);
        }

        private void CheckForPause(Action callback)
        {
            if (GameUI.IsPause)
            {
                DeltaMouseX = 0f;
                DeltaMouseY = 0f;
                return;
            }
            
            callback?.Invoke();
        }
    }
}