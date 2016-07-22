using System;
using System.Collections.Generic;
using System.Linq;

namespace RailwayWars.SampleBot
{
    internal class GameBoardState
    {
        private readonly GameStateDTO _gameState;
        private readonly string _myId;

        public int MyMoney { get; }
        public IEnumerable<FreeCellDTO> FreeCells => _gameState.FreeCells;

        public GameBoardState(GameStateDTO gameState, string myId)
        {
            _myId = myId;
            _gameState = gameState;
            MyMoney = _gameState.Railways.First(p => p.Id == _myId).Money;
        }

        //================================================
        // Add helper methods to interpret the board here
        //================================================
    }
}
