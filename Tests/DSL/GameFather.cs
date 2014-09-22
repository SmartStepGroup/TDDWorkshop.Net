using FakeItEasy;

namespace Tests.DSL
{
    public class GameFather
    {
        private Game game = new Game();

        public GameFather Started()
        {
            game.Start();
            return this;
        }

        public GameFather WithLuckyScore(int luckyScore)
        {
            game.luckyScore = luckyScore;
            var cheatDice = A.Fake<IDice>();
            A.CallTo(() => cheatDice.Roll()).Returns(luckyScore);
            game.dice = cheatDice;

            return this;
        }

        public static implicit operator Game(GameFather father)
        {
            return father.game;
        }
    }
}