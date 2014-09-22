using System;
using NUnit.Framework;
using Tests.DSL;

namespace Tests
{
    public class BaseTest
    {
        protected static Father Create;
        public Game CreateGame()
        {
            return new Game();
        }

        public Player CreatePlayer()
        {
            return new Player();
        }

        public Bet CreateBet(int diceValue = 1, int chipCount = 0)
        {
            return new Bet(diceValue, chipCount);
        }
    }

    public static class ExteptionExtensions
    {
        public static void WithMessage(this Exception e, string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }

    public static class IntExtensions
    {
        public static int Chips(this int chips)
        {
            return chips;
        }

        public static Bet On(this int chips, int score)
        {
            return new Bet(diceValue: score, chipsCount: chips);
        }
    }
}