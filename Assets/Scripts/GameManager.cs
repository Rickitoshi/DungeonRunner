using System;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbyCamera;

    private void Start()
    {
        Application.targetFrameRate = 120;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerController.Instance.State = PlayerState.Run;
            lobbyCamera.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerController.Instance.State = PlayerState.Idle;
            lobbyCamera.SetActive(true);
        }
    }
}
