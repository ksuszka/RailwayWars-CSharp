namespace RailwayWars.SampleBot
{
    public class Offer
    {

        public int X { get; }
        public int Y { get; }
        public int Price { get; }
        public string Command => $"BUY {X} {Y} {Price}";

        public Offer(int x, int y, int price)
        {
            Price = price;
            Y = y;
            X = x;
        }
    }
}
