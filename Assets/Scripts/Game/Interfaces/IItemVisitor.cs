public interface IItemVisitor
{
        public void ItemVisit(CoinItem coinItem);
        public void ItemVisit(MagnetItem magnetItem);
}
