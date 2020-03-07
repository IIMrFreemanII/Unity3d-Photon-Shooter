using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameMenuLoader : MonoBehaviour
{
    private void Start()
    {
        OnInitialUiLoad();
    }
    
    private void OnInitialUiLoad()
    {
        if (SceneManager.GetSceneByName("MainGameMenuScene").isLoaded == false) {
            SceneManager.LoadSceneAsync("MainGameMenuScene", LoadSceneMode.Additive);
        }
    }
}
