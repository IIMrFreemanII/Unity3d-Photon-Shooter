using UnityEngine;
using UnityEngine.UI;

namespace MyGame
{
    public class QuitButton : MonoBehaviour
    {
        private Button _quitButton = null;

        private void Awake()
        {
            _quitButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _quitButton.onClick.AddListener(OnQuitGame);
        }
    
        private void OnDisable()
        {
            _quitButton.onClick.RemoveListener(OnQuitGame);
        }

        private void OnQuitGame()
        {
            print("Quit");
            Application.Quit();
        }
    }
}
