using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static void Switch(string sceneName)
    {
        sceneName = sceneName.Trim();
        
        if (sceneName.ToLower() == "quit") Application.Quit(0);
        else SceneManager.LoadScene(sceneName);
    }
}