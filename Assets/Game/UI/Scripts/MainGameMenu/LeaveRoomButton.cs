using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class LeaveRoomButton : MonoBehaviour
{
    [SerializeField] private Button leaveButton;

    private void Awake()
    {
        leaveButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        leaveButton.onClick.AddListener(LeaveRoom);
    }
    private void OnDisable()
    {
        leaveButton.onClick.RemoveListener(LeaveRoom);
    }

    private void LeaveRoom()
    {
        if (!PhotonNetwork.InRoom)
        {
            Debug.LogWarning("You are not in the room.");
            return;
        }
        
        PhotonNetwork.LeaveRoom();
    }
}
