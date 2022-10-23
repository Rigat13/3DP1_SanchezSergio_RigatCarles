using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] KeyCode startKey = KeyCode.Space;
    [SerializeField] Animator canvasAnimator;
    [SerializeField] GameManager gameManager;

    void Update()
    {
        if (Input.GetKeyDown(startKey))
        {
            startGame();
        }
    }

    void startGame()
    {
        canvasAnimator.SetTrigger("StartGame");
        gameManager.StartGame();
    }
}
