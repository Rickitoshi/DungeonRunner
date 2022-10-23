
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    
    protected virtual void Start()
    {
        Subscribe();
    }

    protected virtual void OnDestroy()
    {
        Unsubscribe();
    }

    protected abstract void Subscribe();

    protected abstract void Unsubscribe();
    
    
    public void Activate()
    {
        gameObject.SetActive(true);
    }
    
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
