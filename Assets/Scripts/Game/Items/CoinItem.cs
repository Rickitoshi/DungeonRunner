using UnityEngine;

public class CoinItem : Item
{
    [SerializeField] private int value = 5;

    public int Value => value;
    
    protected override void Visit(IItemVisitor itemVisitor)
    {
        itemVisitor.ItemVisit(this);
    }
}
