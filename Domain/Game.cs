namespace Domain {
    public class Game {
        private int PlayersCount = 0;


        public bool AddNewPlayer() {
            this.PlayersCount++;
            return true;
        }
    }
}