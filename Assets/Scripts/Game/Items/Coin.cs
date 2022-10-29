using UnityEngine;

public class Coin : Item
{
    [SerializeField] private int Cost;
    
    protected override void Visit(IItemVisitor itemVisitor)
    {
        itemVisitor.ItemVisit(this,Cost);
    }
}
