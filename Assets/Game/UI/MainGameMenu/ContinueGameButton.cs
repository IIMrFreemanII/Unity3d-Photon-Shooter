using UnityEngine;
using UnityEngine.UI;

public class ContinueGameButton : MonoBehaviour
{
    [SerializeField] private MainGameMenuCanvas mainGameMenuCanvas;
    [SerializeField] private Button continueButton;

    private void Awake()
    {
        continueButton = GetComponent<Button>();
    }

    private void Start()
    {
        mainGameMenuCanvas = GameUI.Instance.mainGameMenuContainer.mainGameMenuCanvas;
    }

    private void OnEnable()
    {
        continueButton.onClick.AddListener(ContinueGame);
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(ContinueGame);
    }

    private void ContinueGame()
    {
        mainGameMenuCanvas.gameObject.SetActive(false);
        GameUI.IsPause = false;
    }
}
