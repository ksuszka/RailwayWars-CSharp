using System;
using System.Collections.Generic;
using System.Linq;

namespace RailwayWars.SampleBot
{
    internal class BotEngine
    {
        private readonly string _myId;
        private readonly Random _random = new Random();

        public BotEngine(string myId)
        {
            _myId = myId;
        }

        public IEnumerable<Offer> GetNextMove(GameBoardState gameBoardState)
        {
            //==========================
            // Your bot logic goes here
            //==========================

            Console.WriteLine($"Money: {gameBoardState.MyMoney}");

            var offers = new List<Offer>();

            var moneyLeft = gameBoardState.MyMoney;
            var cellsToBuy = gameBoardState.FreeCells.ToList();
            while (true)
            {
                cellsToBuy = cellsToBuy.Where(c => c.Price <= moneyLeft).OrderBy(_ => _random.NextDouble()).ToList();
                if (cellsToBuy.Count() == 0)
                {
                    break;
                }

                var cell = cellsToBuy.First();
                var price = cell.Price + _random.Next(0, 10);
                offers.Add(new Offer(cell.X, cell.Y, price));
                moneyLeft -= price;
                cellsToBuy = cellsToBuy.Skip(1).ToList();
            }

            return offers;
        }
    }
}
