using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject LobbyCamera;

    public void SetLobbyCamera(bool value)
    {
        LobbyCamera.SetActive(value);
    }
}
