using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace minimax.connectfour
{
    public class State
    {
        public static readonly int EMPTY = -1;
        public static readonly int BLUE = (int)Player.Blue;
        public static readonly int RED = (int)Player.Red;

        private int[,] board;
        private Player currentPlayer;
        private int rows;
        private int cols;

        public State(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
                throw new ArgumentOutOfRangeException("Rows and cols must be positive");
            this.rows = rows;
            this.cols = cols;

            board = new int[Rows, Cols];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    board[i, j] = EMPTY;
                }
            }

            currentPlayer = Player.Blue; //Decidiamo che inizia sempre il blu, non è importante
        }

        public State(int[,] board, Player player)
        {
            this.rows = board.GetLength(0);
            this.cols = board.GetLength(1);

            this.board = new int[Rows, Cols];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (board[i, j] != EMPTY && board[i, j] != BLUE && board[i, j] != RED)
                        throw new ArgumentException("Illegal board state");
                    this.board[i, j] = board[i, j];
                }
            }

            this.currentPlayer = player;
        }

        public Player CurrentPlayer
        {
            get { return currentPlayer; }
        }

        public int[,] Board
        {
            get { return (int[,]) board.Clone(); }
        }

        public int Rows
        {
            get { return rows; }
        }

        public int Cols
        {
            get { return cols; }
        }

        public int GetBoardValue(int row, int col)
        {
            if (row < 0 || row > Rows)
                throw new ArgumentOutOfRangeException("Illegal row");
            if (col < 0 || col > Cols)
                throw new ArgumentOutOfRangeException("Illegal col");

            return board[row, col];
        }

        public State GetResultingState(Action action)
        {
            //Apply action to the state
            int[,] newBoard = (int[,]) board.Clone();
            newBoard[action.Row, action.Col] = (int) currentPlayer;

            Player player;
            //Switch player
            if (currentPlayer == Player.Blue)
                player = Player.Red;
            else
                player = Player.Blue;

            return new State(newBoard, player);
        }

        override public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<int, string> playerDict = new Dictionary<int, string>();
            playerDict[EMPTY] = " ";
            playerDict[BLUE] = "B";
            playerDict[RED] = "E";

            sb.Append("Current player: ");
            sb.Append(playerDict[(int)currentPlayer]);

            sb.Append("\nBoard:\n");

            for (int i = Rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < Cols; j++)
                {
                    sb.Append(playerDict[board[i, j]]);
                    if (j != Cols - 1)
                        sb.Append("|");
                    else
                        sb.Append("\n");
                }
                if (i != 0)
                    sb.Append(String.Concat(Enumerable.Repeat("-", 2*Cols-1).ToArray()) + "\n");
            }
            sb.Append("\n");

            return sb.ToString();
        }
    }
}
