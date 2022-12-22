using System;
using Cinemachine;
using Signals;
using UnityEngine;
using Zenject;

public class GameHelper : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playCamera;
    [SerializeField] private CinemachineVirtualCamera lobbyCamera;
    [SerializeField] private CinemachineVirtualCamera marketCamera;
    [SerializeField] private CinemachineVirtualCamera upgradeCamera;
    
    public static GameHelper Instance { get; private set; }

    public CameraState CameraState
    {
        set
        {
            if(value==_currentCameraState) return;
                
            switch (value)
            {
                case CameraState.Lobby:
                    ChangeCamera(lobbyCamera);
                    break;
                case CameraState.Run:
                    ChangeCamera(playCamera);
                    break;
                case CameraState.Market:
                    ChangeCamera(marketCamera);
                    break;
                case CameraState.Upgrade:
                    ChangeCamera(upgradeCamera);
                    break;
            }

            _currentCameraState = value;
        }
    }

    private CameraState _currentCameraState;
    private CinemachineVirtualCamera _currentCamera;
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

    private void Start()
    {
        InitializeCamera(lobbyCamera);
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

    private void InitializeCamera(CinemachineVirtualCamera startCamera)
    {
        lobbyCamera.Priority = 1;
        playCamera.Priority = 1;
        marketCamera.Priority = 1;

        _currentCamera = startCamera;
        _currentCamera.Priority = 2;
    }
    
    private void ChangeCamera(CinemachineVirtualCamera newCamera)
    {
        _currentCamera.Priority = 1;
        _currentCamera = newCamera;
        _currentCamera.Priority = 2;
    }
}

public enum CameraState
{
    None,
    Lobby,
    Run,
    Market,
    Upgrade
}