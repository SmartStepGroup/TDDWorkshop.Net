using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    public class Player
    {
        private Game currentGame;

        public void Enter(Game m_game)
        {
            if (currentGame != null) throw new InvalidOperationException("Можешь играть в одну игру ты только, юный падован");
            m_game.CheckMaxPlayers();

            currentGame = m_game;
            m_game.Join();
        }

        public bool IsIn(Game m_game)
        {
            return currentGame == m_game;
        }

        public void Exit()
        {
            if (currentGame == null) throw new InvalidOperationException("Не войдя не можешь ты выйти, юный падован");
            currentGame.Leave();
            currentGame = null;
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