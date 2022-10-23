using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] FPSController playerController;
    [SerializeField] RaycastShooting playerShooter;

    void Start()
    {
        playerController.enabled = false;
        playerShooter.enabled = false;
    }

    public void StartGame()
    {
        playerController.enabled = true;
        playerShooter.enabled = true;
    }
}
