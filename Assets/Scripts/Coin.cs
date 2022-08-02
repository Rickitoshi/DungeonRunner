using UnityEngine;

public class Coin : Item
{
    [SerializeField] private int Cost;

    private void Update()
    {
        if (isVisitorDetected(out IItemVisitor itemVisitor))
        {
            itemVisitor.Visit(this, Cost);
        }
    }
    
}
