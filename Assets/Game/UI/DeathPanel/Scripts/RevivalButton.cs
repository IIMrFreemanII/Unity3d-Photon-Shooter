using UnityEngine;
using UnityEngine.UI;

public class RevivalButton : MonoBehaviour
{
    public Button revivalButton;

    public void Initialize()
    {
        revivalButton = GetComponent<Button>();
    }
}
