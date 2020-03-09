using UnityEngine;
using UnityEngine.UI;

public class RevivalButton : MonoBehaviour
{
    public Button button;

    public void Initialize()
    {
        button = GetComponent<Button>();
    }
}
