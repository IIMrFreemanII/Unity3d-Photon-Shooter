using UnityEngine;

public class PlayerGameUIController : MonoBehaviour
{
    private MainGameMenuCanvas _mainGameMenuCanvas;
    
    private void Start()
    {
        _mainGameMenuCanvas = GameUI.Instance.mainGameMenuContainer.mainGameMenuCanvas;
        HideUIOnStart();
    }

    private void Update()
    {
        ToggleMainMenu();
    }
    
    private void ToggleMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _mainGameMenuCanvas.gameObject.SetActive(!_mainGameMenuCanvas.gameObject.activeInHierarchy);
            GameUI.IsPause = !GameUI.IsPause;
        }
    }
    
    private void HideUIOnStart()
    {
        _mainGameMenuCanvas.gameObject.SetActive(false);
        GameUI.IsPause = false;
    }
}
