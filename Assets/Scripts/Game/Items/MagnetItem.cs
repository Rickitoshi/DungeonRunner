using UnityEngine;

public class MagnetItem : Item
{
    protected override void Visit(IItemVisitor itemVisitor)
    {
        itemVisitor.ItemVisit(this);
    }
}
