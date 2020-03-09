using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields

            public static GameManager Instance;
            [SerializeField] private bool isOfflineMode = false;
            public static bool IsOfflineMode = true;
        
        #endregion

        #region MonoBehaviour Callbacks

            private void Awake()
            {
                Instance = this;
                IsOfflineMode = isOfflineMode;
            }
            
            private void OnApplicationQuit()
            {
                if (IsOfflineMode) return;
                
                LeaveRoom();
                PhotonNetwork.Disconnect();
            }
            
        #endregion

        #region Photon Callbacks

            public override void OnLeftRoom()
            {
                SceneManager.LoadScene("StartMainMenuScene");
            }
            
            public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
            {
                Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName); // not seen if you're the player connecting

                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                }
            }

            public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
            {
                Debug.LogFormat("OnPlayerLeftRoom() {0}", otherPlayer.NickName); // seen when other disconnects

                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                }
            }

            // public void OnPhotonInstantiate(PhotonMessageInfo info)
            // {
            //     print($"Instantiate: {info.Sender.NickName}");
            // }

            #endregion

        #region Public Methods

            private void LeaveRoom()
            {
                PhotonNetwork.LeaveRoom();
            }

        #endregion
    }
}
