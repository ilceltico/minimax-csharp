using System;
using Xunit;
using minimax.connectfour;

namespace tests.connectfour
{
    public class GameTest
    {
        [Fact]
        public void Test1()
        {
            int[,] board = new int[6, 7] {  {  0, 0, 0, 0,-1,-1,-1},
                                            { -1,-1,-1,-1,-1,-1,-1},
                                            { -1,-1,-1,-1,-1,-1,-1},
                                            { -1,-1,-1,-1,-1,-1,-1},
                                            { -1,-1,-1,-1,-1,-1,-1},
                                            { -1,-1,-1,-1,-1,-1,-1} };
            State state = new State(board, Player.Red);
            Game game = new Game(6, 7);

            Assert.True(game.HasWon(state, Player.Blue));
            Assert.False(game.HasWon(state, Player.Red));
        }
    }
}
