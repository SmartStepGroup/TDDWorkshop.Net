using System;

namespace Domain
{
    public class Player
    {
        private Game activeGame;
        private int coins;

        public void Enter(Game game)
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


        public int GetAvailableCoins()
        {
            return this.coins;
        }

        public int BuyCoins (int newCoins)
        {
            return this.coins += newCoins;
        }


        public void MakeBet( int roll,int amount)
        {
            if (amount > this.coins)
            {
               throw new ArgumentException("������ ������ ��� � ������ �����!");
            }
            activeGame.MakeBet(this,roll, amount);
            this.coins -= amount;
        }

        public void CancelAllBets()
        {
            
        }

        public bool HasAnyBet()
        {
            return false;
        }

        public void TakeWinCoins(int i)
        {
            this.coins += i;
        }
    }

}