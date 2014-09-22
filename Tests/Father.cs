using Domain;

namespace Tests {
    public class Father {
        public PlayerFather Player {
            get {
                return new PlayerFather();
            }
        }
    
        public GameFather Game  {
            get {
                return new GameFather();
            }
        }
    }

    public class GameFather {
        public static implicit operator Game(GameFather father)
        {
            return new Game();
        }
    }

    public class PlayerFather {
        private Game game;
        private int chipsCount;
        private Bet bet;

        public static implicit operator Player(PlayerFather father) {
            var player = new Player();
            if (father.game != null) player.setActiveGame(father.game);
            if (father.chipsCount != null) player.byeChips(father.chipsCount);
            if (father.bet != null) player.bets(father.bet);

            return player;
        }

        public PlayerFather In(Game game) {
            this.game = game;
            return this;
        }

        public PlayerFather With(int cheaps = 5) {
            this.chipsCount = cheaps;
            return this;
        }

        public PlayerFather Bets(Bet bet) {
            this.bet = bet;
            return this;
        }
    }
}