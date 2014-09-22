using Domain;
using FakeItEasy;

namespace Tests
{
    public class Builder
    {
        private const int unlucky_roll = 4;
        private const int lucky_roll = 6;

        public Builder()
        {
            DiceGame = new DiceGameBuilder();
            Player = new PlayerBuilder();
            Casino = new CasinoBuilder();
            UnluckyDice = A.Fake<IDice>();
            A.CallTo(() => UnluckyDice.Roll()).Returns(unlucky_roll);
            LuckyDice = A.Fake<IDice>();
            A.CallTo(() => LuckyDice.Roll()).Returns(lucky_roll);
        }

        public DiceGameBuilder DiceGame { get; private set; }
        public PlayerBuilder Player { get; private set; }
        public IDice UnluckyDice { get; private set; }
        public IDice LuckyDice { get; private set; }
        public CasinoBuilder Casino { get; private set; }
    }
}