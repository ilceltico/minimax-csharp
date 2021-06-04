using System;
using System.Collections.Generic;
using minimax.core.adversarial;

namespace minimax.connectfour
{
    public class Game : IGame<State, Action, Player>
    {

        public static readonly int ROWS = 6;
        public static readonly int COLS = 7;

        private int rows;
        private int cols;

        public Game(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
        }

        public Game()
        {
            new Game(ROWS, COLS);
        }

        public int Rows
        {
            get { return rows; }
        }

        public int Cols
        {
            get { return cols; }
        }

        public List<Action> GetActions(State state)
        {
            //Genera un'azione per ogni posizione libera della griglia
            List<Action> result = new List<Action>();

            for (int i = 0; i < state.Rows; i++)
            {
                for (int j = 0; j < state.Cols; j++)
                {
                    //Posso mettere una pedina solo se e' la riga più bassa o se c'e' un'altra pedina sotto
                    if (i == 0 || state.GetBoardValue(i - 1, j) != State.EMPTY)
                        //Devo comunque controllare che la casella sia libera
                        if (state.GetBoardValue(i, j) == State.EMPTY)
                            result.Add(new Action(i, j));
                }
            }

            return result;
        }

        public State GetInitialState()
        {
            return new State(ROWS, COLS);
        }

        public Player GetPlayer(State state)
        {
            return state.CurrentPlayer;
        }

        public Player[] GetPlayers()
        {
            return new Player[2] { Player.Blue, Player.Red };
        }

        public State GetResult(State state, Action action)
        {
            //Apply action to the state
            int[,] newBoard = (int[,])state.Board.Clone();
            newBoard[action.Row, action.Col] = (int)state.CurrentPlayer;

            Player player;
            //Switch player
            if (state.CurrentPlayer == Player.Blue)
                player = Player.Red;
            else
                player = Player.Blue;

            return new State(newBoard, player);
        }

        public double GetUtility(State state, Player player)
        {
            if (HasWon(state, player))
                return double.PositiveInfinity;

            Player otherPlayer;
            if (player == Player.Blue)
                otherPlayer = Player.Red;
            else
                otherPlayer = Player.Blue;

            if (HasWon(state, otherPlayer))
                return double.NegativeInfinity;

            //Se nessuno dei due ha vinto (quindi se c'è un pareggio o se la partita non è ancora finita)
            return 0;

        }

        public bool HasWon(State state, Player player)
        {
            int count;

            //Controllo verticale
            for (int col = 0; col < state.Cols; col++)
            {
                count = 0;
                for (int row = 0; row < state.Rows; row++)
                {
                    if (state.GetBoardValue(row, col) == (int)player)
                        count++;
                    if (count >= 4)
                        return true;
                    if (state.GetBoardValue(row, col) != (int)player)
                        count = 0;
                }
            }

            //Controllo orizzontale
            for (int row = 0; row < state.Rows; row++)
            {
                count = 0;
                for (int col = 0; col < state.Cols; col++)
                {
                    if (state.GetBoardValue(row, col) == (int)player)
                        count++;
                    if (count >= 4)
                        return true;
                    if (state.GetBoardValue(row, col) != (int)player)
                        count = 0;
                }
            }

            //Controllo diagonali principali
            for (int row = 0; row < state.Rows; row++)
            {
                count = 0;
                for (int i = 0; i < state.Cols && row - i >= 0; i++)
                {
                    if (state.GetBoardValue(row - i, i) == (int)player)
                        count++;
                    if (count >= 4)
                        return true;
                    if (state.GetBoardValue(row - i, i) != (int)player)
                        count = 0;
                }
            }
            for (int col = 0; col < state.Cols; col++)
            {
                count = 0;
                for (int i = 0; i < state.Rows && col + i < state.Cols; i++)
                {
                    if (state.GetBoardValue(state.Rows - 1 - i, col + i) == (int)player)
                        count++;
                    if (count >= 4)
                        return true;
                    if (state.GetBoardValue(state.Rows - 1 - i, col + i) != (int)player)
                        count = 0;
                }
            }

            //Controllo diagonali secondarie
            for (int row = 0; row < state.Rows; row++)
            {
                count = 0;
                for (int i = 0; i < state.Cols && row - i >= 0; i++)
                {
                    if (state.GetBoardValue(row - i, state.Cols - 1 - i) == (int)player)
                        count++;
                    if (count >= 4)
                        return true;
                    if (state.GetBoardValue(row - i, state.Cols - 1 - i) != (int)player)
                        count = 0;
                }
            }
            for (int col = 0; col < state.Cols; col++)
            {
                count = 0;
                for (int i = 0; i < state.Rows && col - i >= 0; i++)
                {
                    if (state.GetBoardValue(state.Rows - 1 - i, col - i) == (int)player)
                        count++;
                    if (count >= 4)
                        return true;
                    if (state.GetBoardValue(state.Rows - 1 - i, col - i) != (int)player)
                        count = 0;
                }
            }

            return false;
        }

        public bool IsTerminal(State state)
        {
            if (HasWon(state, Player.Blue))
                return true;
            if (HasWon(state, Player.Red))
                return true;

            //Se la griglia e' piena
            bool full = true;
            for (int row = 0; row < state.Rows; row++)
            {
                for (int col = 0; col < state.Cols; col++)
                {
                    if (state.GetBoardValue(row, col) == State.EMPTY)
                        full = false;
                }
            }

            return full;
        }
    }
}
