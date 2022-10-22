using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Retry()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }

}
