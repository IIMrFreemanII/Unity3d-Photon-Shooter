using System.Collections;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class TrackCurrentNetworkPosition : MonoBehaviour, IPunInstantiateMagicCallback
{
    private Vector3 _lastPosition = PlayerManager.DefaultSpawnPosition;
    private Quaternion _lastRotation = PlayerManager.DefaultSpawnRotation;
    
    private Vector3 _positionToSpawn = PlayerManager.DefaultSpawnPosition;
    private Quaternion _rotationToSpawn = PlayerManager.DefaultSpawnRotation;
    
    private string _playerPositionKey;
    private string _playerRotationKey;

    private PhotonView _photonView;
    private readonly Hashtable _hash = new Hashtable();

    [SerializeField] private float delay = 1f;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();

        _playerPositionKey = $"{_photonView.Owner.NickName}:{_photonView.ViewID} last position";
        _playerRotationKey = $"{_photonView.Owner.NickName}:{_photonView.ViewID} last rotation";
        
        if (!_photonView.IsMine) return;

        SynchronizeValues();
    }

    private void Start()
    {
        if (!_photonView.IsMine) return;

        StartCoroutine(CallWithDelay());
    }

    private IEnumerator CallWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            TrackCurrentPositionAndRotation();
        }
    }
    
    private void SynchronizeValues()
    {
        _hash[_playerPositionKey] = _lastPosition;
        _hash[_playerRotationKey] = _lastRotation;

        PhotonNetwork.LocalPlayer.SetCustomProperties(_hash);
    }

    private void TrackCurrentPositionAndRotation()
    {
        if (!_photonView.IsMine) return;
        
        if (_lastPosition != transform.position || _lastRotation != transform.rotation)
        {
            // print("position has been changed");
            _lastPosition = transform.position;
            _lastRotation = transform.rotation;

            SynchronizeValues();
        }
    }
    
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info.Sender.CustomProperties.ContainsKey(_playerPositionKey))
        {
            _positionToSpawn = (Vector3) info.Sender.CustomProperties[_playerPositionKey];

            if (!_photonView.IsMine)
            {
                transform.position = _positionToSpawn;
            }
        }
        else
        {
            Debug.LogWarning($"No such key: {_playerPositionKey}");
        }
            
        if (info.Sender.CustomProperties.ContainsKey(_playerRotationKey))
        {
            _rotationToSpawn = (Quaternion) info.Sender.CustomProperties[_playerRotationKey];

            if (!_photonView.IsMine)
            {
                transform.rotation = _rotationToSpawn;
            }
        }
        else
        {
            Debug.LogWarning($"No such key: {_playerRotationKey}");
        }
            
        // Debug.Log($"{_photonView.Owner.NickName}:{info.photonView.ViewID} is Instantiated. Position: {_positionToSpawn} Rotation: {_rotationToSpawn.eulerAngles}");
    }
}
