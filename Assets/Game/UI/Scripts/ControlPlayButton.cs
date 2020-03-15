using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame
{
    public class ControlPlayButton : MonoBehaviour
    {
        private Button _playButton;
        private NetworkManager _networkManager;

        private void Awake()
        {
            _playButton = GetComponent<Button>();
            _networkManager = FindObjectOfType<NetworkManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _networkManager.Connect();
            }
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(_networkManager.Connect);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(_networkManager.Connect);
        }
    }
}
