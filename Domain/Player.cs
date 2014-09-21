using System;

namespace Domain
{
    public class Player
    {
        private Game activeGame;
        public void  Enter(Game game)
        {
            if (activeGame != null && activeGame != game)
            {
                throw new InvalidOperationException("����� ����� ���� ������ � ����� ����!!!");
            }
            activeGame = game;
            game.Join(this);
        }

        public bool IsIn(Game game)
        {
            return activeGame == game;
        }

        public void Exit()
        {
            if (activeGame == null) throw new InvalidOperationException("������ ����� �� ����, �� ����� � ��");
            activeGame.Leave(this);
            activeGame = null;
        }

        public void MakeBet(int coins)
        {
            activeGame.MakeBet(this, coins);
        }
    }
}