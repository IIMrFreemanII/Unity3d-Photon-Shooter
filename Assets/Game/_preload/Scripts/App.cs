using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGame
{
    public class App : MonoBehaviour
    {
        #region Public Fields

            public static App Instance;

            // [SerializeField]
            // private PlayerData playerData;
            // public PlayerData PlayerData => playerData;

            // [SerializeField]
            // private WeaponsData weaponsData;
            // public WeaponsData WeaponsData => weaponsData;

        #endregion

        #region Monobehavior Callbacks

            private void Awake()
            {
                Instance = this;

                // playerData = FindObjectOfType<PlayerData>();
                // weaponsData = FindObjectOfType<WeaponsData>();
            }

            private void Start()
            {
                SceneManager.LoadScene("StartMainMenuScene");
            }

        #endregion
    }
}
