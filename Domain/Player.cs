using System;

namespace Domain {
    public class Player {
        private Game activeGame;
        private int ChipsBallance;
        private bool AvaliableBets;

        public void Enter(Game game) {
            if (activeGame == game)
                throw new InvalidOperationException("Ты уже в этой игре!");
            if (activeGame != null)
                throw new InvalidOperationException("Нельзя войти во вторую игру");
            activeGame = game;
            game.AddNewPlayer();
        }

        public bool IsIn(Game game) {
            return game == activeGame;
        }

        public void Exit() {
            if (activeGame == null) throw new InvalidOperationException("Нельзя просто так выйти из игры не войдя");
            activeGame = null;
        }

        public void BuyChips(int chipsCount) {
            if (chipsCount < 1) throw new InvalidOperationException("Нельзя купить количество фишек меньше 1");
            ChipsBallance += chipsCount;
        }

        public int AvailiableChips() {
            return ChipsBallance;
        }

        public bool HasBet() {
           return AvaliableBets;
        }

        public void MakeBet(int bet) {
            AvaliableBets = true;
        }
    }
}