using MyGame;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData = null;

    public static  Vector3 DefaultSpawnPosition = new Vector3(0f, 5f, 0f);
    public static Quaternion DefaultSpawnRotation = Quaternion.identity;
    
    private void Start()
    {
        PhotonNetwork.Instantiate(playerData.playerPrefab.name, DefaultSpawnPosition, DefaultSpawnRotation);
    }
}
