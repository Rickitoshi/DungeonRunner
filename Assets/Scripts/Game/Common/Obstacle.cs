
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool IsActive => gameObject.activeSelf;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IObstacleVisitor obstacleVisitor))
        {
            obstacleVisitor.ObstacleVisit(gameObject.GetComponentInParent<RoadPart>());
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
