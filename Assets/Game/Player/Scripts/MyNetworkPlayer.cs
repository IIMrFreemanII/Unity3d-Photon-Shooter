using Photon.Pun;
using UnityEngine;

namespace MyGame
{
    [RequireComponent(typeof(PhotonView))]
    public class MyNetworkPlayer : MonoBehaviour
    {
        public static MyNetworkPlayer MyLocalNetworkPlayerInstance;
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();

            if (_photonView.IsMine)
            {
                MyLocalNetworkPlayerInstance = this;
            }
        }

        // private void Start()
        // {
        //     CameraHandler cameraHandler = GetComponent<CameraHandler>();
        //
        //     if (cameraHandler != null)
        //     {
        //         if (_photonView.IsMine)
        //         {
        //             cameraHandler.OnStartFollowing();
        //         }
        //     }
        // }
    }
}