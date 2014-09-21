using System;
using System.Security.Policy;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void EnterGame_Player_InGame()
        {
            var player = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);

            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void ExitGame_Player_NotInGame()
        {
            var player = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);
            player.Exit();

            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void ByDefault_ExitGame_Player_NotInGame()
        {
            var player = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);
            player.Exit();

            var e = Assert.Throws<InvalidOperationException>(player.Exit);
            Assert.AreEqual("Не войдя не можешь ты выйти, юный падован", e.Message);
        }

        [Test]
        public void OneGame_Player_InGame()
        {
            var player = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);

            var e = Assert.Throws<InvalidOperationException>(() => player.Enter(game));
            Assert.AreEqual("Можешь играть в одну игру ты только, падован", e.Message);
        }

        [Test]
        public void JoinGameWhereHasPlayer_AnotherPlayer_InGame()
        {
            var player = CreatePlayer();
            var anotherPlayer = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);
            anotherPlayer.Enter(game);

            Assert.IsTrue(anotherPlayer.IsIn(game));
        }

        [Test]
        public void EnterGame_Game_Max6Players()
        {
            var game = CreateGame();
            var player = CreatePlayer();
            var player2 = CreatePlayer();
            var player3 = CreatePlayer();
            var player4 = CreatePlayer();
            var player5 = CreatePlayer();
            var player6 = CreatePlayer();
            var player7 = CreatePlayer();

            player.Enter(game);
            player2.Enter(game);
            player3.Enter(game);
            player4.Enter(game);
            player5.Enter(game);
            player6.Enter(game);

            var e = Assert.Throws<InvalidOperationException>(() => player7.Enter(game));
            Assert.AreEqual("В игре число игроков максимальное", e.Message);
        }

        public Game CreateGame()
        {
            return new Game();
        }

        public Player CreatePlayer()
        {
            return new Player();
        }
    }
}