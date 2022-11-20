
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    
    public bool IsActive => gameObject.activeSelf;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IObstacleVisitor obstacleVisitor))
        {
            obstacleVisitor.ObstacleVisit(damage);
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
