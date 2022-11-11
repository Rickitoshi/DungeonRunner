using System;
using Signals;
using UnityEngine;
using Zenject;

public class GameHelper : MonoBehaviour
{
    [SerializeField] private GameObject playCamera;
    [SerializeField] private GameObject lobbyCamera;
    public static GameHelper Instance { get; private set; }

    public GameObject PlayCamera => playCamera;
    public GameObject LobbyCamera => lobbyCamera;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            _signalBus.Fire<AppFocusChangeSignal>();
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        IronSource.Agent.onApplicationPause(pauseStatus);
    }
}