using Domain;
using FakeItEasy;

namespace Tests
{
    public class Builder
    {
        private const int unlucky_roll = 4;

        public Builder()
        {
            DiceGame = new DiceGameBuilder();
            Player = new PlayerBuilder();
            UnluckyDice = A.Fake<IDice>();
            A.CallTo(() => UnluckyDice.Roll()).Returns(unlucky_roll);
        }

        public DiceGameBuilder DiceGame { get; private set; }
        public PlayerBuilder Player { get; private set; }
        public IDice UnluckyDice { get; private set; }
    }
}