using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace MyGame
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        #region Private Fields

            /// <summary>
            /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
            /// </summary>
            private string _gameVersion = "1";
        
            /// <summary>
            /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon,
            /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
            /// Typically this is used for the OnConnectedToMaster() callback.
            /// </summary>
            private bool isConnecting;

        #endregion

        #region Private Serializable Fields

            /// <summary>
            /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
            /// </summary>
            [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
            [SerializeField]
            private byte maxPlayersPerRoom = 4;
            
            [Tooltip("The Ui Panel to let the user enter name, connect and play")]
            private ControlPanel controlPanel = null;
            private ProgressLabel progressLabel = null;
            private QuitButton quitButton = null;

        #endregion
    
        #region Public Fields

    

        #endregion

        #region MonoBehaviour Callbacks

            private void Awake()
            {
                // #Critical
                // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
                PhotonNetwork.AutomaticallySyncScene = true;
            }
        
            private void Start()
            {
                progressLabel = FindObjectOfType<ProgressLabel>();
                controlPanel = FindObjectOfType<ControlPanel>();
                quitButton = FindObjectOfType<QuitButton>();

                progressLabel.gameObject.SetActive(false);
                controlPanel.gameObject.SetActive(true);
                quitButton.gameObject.SetActive(true);
            }

        #endregion

        #region Public Methods

            /// <summary>
            /// Start the connection process.
            /// - If already connected, we attempt joining a random room
            /// - if not yet connected, Connect this application instance to Photon Cloud Network
            /// </summary>
            public void Connect()
            {
                // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
                // isConnecting = PhotonNetwork.ConnectUsingSettings();
            
                progressLabel.gameObject.SetActive(true);
                controlPanel.gameObject.SetActive(false);
                quitButton.gameObject.SetActive(false);
            
                if (PhotonNetwork.IsConnected)
                {
                    // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                    PhotonNetwork.JoinRandomRoom();
                }
                else
                {
                    // #Critical, we must first and foremost connect to Photon Online Server.
                    isConnecting = PhotonNetwork.ConnectUsingSettings();
                    PhotonNetwork.GameVersion = _gameVersion;
                }
            }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

            public override void OnConnectedToMaster()
            {
                Debug.Log("NetworkLauncher: OnConnectedToMaster() was called by PUN");

                // we don't want to do anything if we are not attempting to join a room.
                // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
                // we don't want to do anything.
                if (isConnecting)
                {
                    // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
                    PhotonNetwork.JoinRandomRoom();
                    isConnecting = false;
                }
            }

            public override void OnDisconnected(DisconnectCause cause)
            {
                progressLabel.gameObject.SetActive(false);
                controlPanel.gameObject.SetActive(true);
            
                Debug.LogWarningFormat("NetworkLauncher: OnDisconnected() was called by PUN with reason {0}", cause);
            }

            public override void OnJoinRandomFailed(short returnCode, string message)
            {
                Debug.Log("NetworkLauncher: OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

                // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
                PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom});
            }

            public override void OnJoinedRoom()
            {
                Debug.Log("NetworkLauncher: OnJoinedRoom() called by PUN. Now this client is in a room.");
            
                // #Critical: We only load if we are the first player, else we rely on `PhotonNetwork.AutomaticallySyncScene` to sync our instance scene.
                // if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
                // {
                //     Debug.Log("We load the 'Room for 1' ");
                //
                //     // #Critical
                //     // Load the Room Level.
                //     PhotonNetwork.LoadLevel("Room for 1");
                // }
                
                PhotonNetwork.LoadLevel("SomeMap");
            }

        #endregion
    }
}
