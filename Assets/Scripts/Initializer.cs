using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Reload();
    }

    public void Reload()
    {
        SceneManager.LoadScene("Gameplay");
    }
}