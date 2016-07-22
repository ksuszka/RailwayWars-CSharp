using System.Collections.Generic;

namespace RailwayWars.SampleBot
{
    public interface ILocation
    {
        int X { get; }
        int Y { get; }
    }

    public class GameStateDTO
    {
        public int Turn { get; set; }
        public int TurnTimeMilliseconds { get; set; }
        public IEnumerable<CellDTO> Cities { get; set; }
        public IEnumerable<FreeCellDTO> FreeCells { get; set; }
        public IEnumerable<RailwayDTO> Railways { get; set; }
        public int TurnProfit { get; set; }
    }

    public struct RailwayDTO
    {
        public string Id { get; set; }
        public int Money { get; set; }
        public int Score { get; set; }
        public IEnumerable<CellDTO> OwnedCells { get; set; }
    }

    public struct CellDTO : ILocation
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public struct FreeCellDTO : ILocation
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Price { get; set; }
    }
}
