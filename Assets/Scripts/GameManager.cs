using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerController.Instance.State = PlayerState.Run;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerController.Instance.State = PlayerState.Idle;
        }
    }
}
