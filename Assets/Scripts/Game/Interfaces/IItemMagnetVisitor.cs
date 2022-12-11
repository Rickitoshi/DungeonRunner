using Game.Systems;

public interface IItemMagnetVisitor
{
    public void MagnetVisit(MagnetSystem magnet, float moveSpeed);
}