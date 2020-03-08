using TMPro;
using UnityEngine;

public class TimeToRevivalText : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    public void SetText(string text)
    {
        tmpText.text = text;
    }
}