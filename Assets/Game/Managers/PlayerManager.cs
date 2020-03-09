using MyGame;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData = null;
    private void Start()
    {
        if (GameManager.IsOfflineMode) return;
        
        PhotonNetwork.Instantiate(playerData.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity);
    }
}
