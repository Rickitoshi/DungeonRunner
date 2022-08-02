public interface IItemVisitor
{
        public void Visit(Coin coin,int cost);
        public void Visit(Magnet magnet);
}
