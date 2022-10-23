using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool ignoreX;
    [SerializeField] private bool ignoreY;
    [SerializeField] private bool ignoreZ;

    void Update()
    {
        Vector3 targetPosition = new Vector3(ignoreX ? 0 : target.position.x, ignoreY ? 0 : target.position.y,
            ignoreZ ? 0 : target.position.z);
        transform.position = targetPosition;
    }
}
