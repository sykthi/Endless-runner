using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static bool isPlayClicked;
    public GameObject playButton;

    private void Start()
    {
        isPlayClicked = false;
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("EndLess");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        isPlayClicked = true;
        Destroy(playButton);
    }
}
