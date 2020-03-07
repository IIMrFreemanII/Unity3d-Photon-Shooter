using Photon.Pun;
using UnityEngine;

namespace MyGame
{
    public class HandleServerDisconnect : MonoBehaviour
    {
        private void OnApplicationQuit()
        {
            PhotonNetwork.Disconnect();
        }
    }
}
