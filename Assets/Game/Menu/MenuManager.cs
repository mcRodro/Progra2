using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void LoadRanking()
    {
        SceneManager.LoadScene("Ranking");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
