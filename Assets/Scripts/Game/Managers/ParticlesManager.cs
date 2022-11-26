using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem spawnParticle;
    [SerializeField] private ParticleSystem pickUpItem;

    private ParticleSystem _spawn;
    private ParticleSystem _pickUpItem;
        
    private void Start()
    {
        _spawn = Instantiate(spawnParticle,transform);
        _pickUpItem = Instantiate(pickUpItem,transform);
    }

    public void SetSpawnParticle(Vector3 position,Vector3 offset)
    {
        _spawn.transform.position = position + offset;
        _spawn.Play();
    }

    public void SetPickUpItemParticle(Vector3 position)
    {
        _pickUpItem.transform.position = position;
        _pickUpItem.Play();
    }
}
