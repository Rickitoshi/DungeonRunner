
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IObstacleVisitor obstacleVisitor))
        {
            obstacleVisitor.Visit();
        }
    }
}
