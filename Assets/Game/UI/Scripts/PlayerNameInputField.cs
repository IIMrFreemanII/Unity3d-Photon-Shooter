using Photon.Pun;
using TMPro;
using UnityEngine;

namespace MyGame
{
    [RequireComponent(typeof(TMP_InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        #region Private Fields

            private TMP_InputField _inputField = null;

        #endregion
        
        #region Private Constants

            // Store the PlayerPref Key to avoid typos
            private const string PlayerNamePrefKey = "PlayerName";

        #endregion

        #region MonoBehaviour Callbacks

            private void Awake()
            {
                _inputField = GetComponent<TMP_InputField>();
            }

            private void Start()
            {
                string defaultName = string.Empty;
                
                if (_inputField != null)
                {
                    if (PlayerPrefs.HasKey(PlayerNamePrefKey))
                    {
                        defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
                        _inputField.text = defaultName;
                    }
                }

                PhotonNetwork.NickName = defaultName;
            }

            private void OnEnable()
            {
                _inputField.onValueChanged.AddListener(SetPlayerName);
            }
            
            private void OnDisable()
            {
                _inputField.onValueChanged.RemoveListener(SetPlayerName);
            }

            #endregion

        #region Private Methods

            /// <summary>
            /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
            /// </summary>
            /// <param name="value">The name of the Player</param>
            private void SetPlayerName(string value)
            {
                // #Important
                if (string.IsNullOrEmpty(value))
                {
                    Debug.LogError("Player Name is null or empty");
                    return;
                }

                PhotonNetwork.NickName = value;
                
                PlayerPrefs.SetString(PlayerNamePrefKey, value);
            }
        
        #endregion
    }
}
