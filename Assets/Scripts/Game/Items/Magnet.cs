using UnityEngine;

public class Magnet : Item
{
    protected override void Visit(IItemVisitor itemVisitor)
    {
        itemVisitor.Visit(this);
    }
}
