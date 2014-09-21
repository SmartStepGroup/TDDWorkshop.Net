using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    public class Player
    {
        private Game currentGame;
        private int chipsCount;

        public void Enter(Game game)
        {
            if (currentGame != null) throw new InvalidOperationException("Можешь играть в одну игру ты только, юный падован");
            game.CheckMaxPlayers();

            currentGame = game;
            game.Join();
        }

        public bool IsIn(Game game)
        {
            return currentGame == game;
        }

        public void Exit()
        {
            if (currentGame == null) throw new InvalidOperationException("Не войдя не можешь ты выйти, юный падован");
            currentGame.Leave();
            currentGame = null;
        }

        public void BuyChips(int count)
        {
            chipsCount += count;
        }

        public decimal GetChipsCount()
        {
            return chipsCount;
        }
    }

    public class Game
    {
        private int playerCount = 0;

        public void Join()
        {
            playerCount++;
        }

        public void Leave()
        {
            playerCount--;
        }
        
        public void CheckMaxPlayers()
        {
            if (playerCount == 6) throw new InvalidOperationException("В игре число игроков максимальное, юный падован"); ;
        }
    }

}