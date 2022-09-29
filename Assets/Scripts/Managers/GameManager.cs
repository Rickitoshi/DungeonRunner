using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbyCamera;
    [SerializeField] private UIManager UIManager;

    private int _coins;

    private void Start()
    {
        Application.targetFrameRate = 120;
        Subscribe();
        UIManager.Initialize(_coins);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerController.Instance.State = State.Run;
            lobbyCamera.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerController.Instance.State = State.Idle;
            lobbyCamera.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        PlayerController.Instance.ItemsCollector.OnPickUpCoin += AddCoins;
        PlayerController.Instance.OnDie += PlayerDie;
    }
    
    private void Unsubscribe()
    {
        PlayerController.Instance.ItemsCollector.OnPickUpCoin -= AddCoins;
        PlayerController.Instance.OnDie -= PlayerDie;
    }

    private void PlayerDie()
    {
        PlayerController.Instance.State = State.Die;
    }
    
    private void AddCoins(int cost)
    {
        if (cost > 0)
        {
            _coins += cost;
            UIManager.AddCoins(cost);
        }
    }
}
