namespace Domain {
    public class Game {
        private int PlayersCount = 0;


        public void AddNewPlayer() {
            this.PlayersCount++;
        }

        public bool HasPlayers() {
            if (PlayersCount > 0)
                return true;
            return false;
        }

    }
}