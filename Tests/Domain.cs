using System;

namespace Tests
{
    public class Player
    {
        private Game currentGame;

        public void Enter(Game game)
        {
            if (currentGame != null) throw new InvalidOperationException("Можешь играть в одну игру ты только, падован");
            game.CheckMaxPlayers();
            currentGame = game;
            game.playerCount++;
        }

        public bool IsIn(Game game)
        {
            return currentGame == game;
        }

        public void Exit()
        {
            if (currentGame == null) throw new InvalidOperationException("Не войдя не можешь ты выйти, юный падован");
            currentGame.playerCount--;
            currentGame = null;
        }
    }

    public class Game
    {
        public int playerCount = 0;

        public void CheckMaxPlayers()
        {
            if (playerCount == 6) throw new InvalidOperationException("В игре число игроков максимальное"); ;
        }
    }

}