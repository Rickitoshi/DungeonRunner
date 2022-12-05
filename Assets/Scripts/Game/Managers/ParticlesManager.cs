using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem spawnParticle;
    [SerializeField] private ParticleSystem pickUpItem;
    [SerializeField] private ParticleSystem runParticle;

    private ParticleSystem _spawn;
    private ParticleSystem _pickUpItem;
    private ParticleSystem _runParticle;
    
    private void Awake()
    {
        _spawn = Instantiate(spawnParticle,transform);
        _pickUpItem = Instantiate(pickUpItem,transform);
        _runParticle = Instantiate(runParticle,transform);
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

    public void GetRunParticle(Transform parent)
    {
        _runParticle.transform.parent = parent;
    }

    public void SetActiveRunParticle(bool value)
    {
        switch (value)
        {
            case true when !_runParticle.isPlaying:
                _runParticle.Play();
                break;
            case false when _runParticle.isPlaying:
                _runParticle.Stop();
                break;
        }
    }
}
