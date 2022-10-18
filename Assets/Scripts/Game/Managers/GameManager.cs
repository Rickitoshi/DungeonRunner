using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbyCamera;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private RoadManager roadManager;

    private int _coins;

    private void Start()
    {
        Subscribe();
        UIManager.Initialize();
        UIManager.GameScreen = GameScreen.Menu;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerController.Instance.State = State.Run;
            lobbyCamera.SetActive(false);
            UIManager.GameScreen = GameScreen.Game;
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
        UIManager.OnPause += Pause;
        UIManager.OnResume += Resume;
        UIManager.OnMenu += Restart;
    }
    
    private void Unsubscribe()
    {
        PlayerController.Instance.ItemsCollector.OnPickUpCoin -= AddCoins;
        PlayerController.Instance.OnDie -= PlayerDie;
        UIManager.OnPause -= Pause;
        UIManager.OnResume -= Resume;
        UIManager.OnMenu -= Restart;
    }

    private void Pause()
    {
        PlayerController.Instance.State = State.Idle;
    }

    private void Resume()
    {
        PlayerController.Instance.State = State.Run;
    }

    private void Restart()
    {
        lobbyCamera.SetActive(true);
        PlayerController.Instance.SetDefault();
        roadManager.Restart();
    }
    
    private void PlayerDie()
    {
        PlayerController.Instance.State = State.Die;
        UIManager.GameScreen = GameScreen.Lose;
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
