namespace Domain {
    public class Bet {
        public Bet(int chips, int score) {
            Chips = chips;
            Score = score;
        }

        public int Chips { get; private set; }
        public int Score { get; private set; }

        #region Equality

        protected bool Equals(Bet other) {
            return Chips == other.Chips && Score == other.Score;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Bet) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (Chips*397) ^ Score;
            }
        }

        #endregion
    }
}