using UnityEngine;

public class Magnet : Item
{
    [SerializeField] private float duration = 5;
    [SerializeField] private float magnetizationRate = 9;

    public float Duration => duration;
    public float MagnetizationRate => magnetizationRate;


    protected override void Visit(IItemVisitor itemVisitor)
    {
        itemVisitor.ItemVisit(this);
    }
}
