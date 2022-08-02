using UnityEngine;

public class Magnet : Item
{
    private void Update()
    {
        if (isVisitorDetected(out IItemVisitor itemVisitor))
        {
            itemVisitor.Visit(this);
        }
    }
}
