using System;
using System.Collections.Generic;
using minimax.core.adversarial;

namespace minimax.tictactoe
{
    public class Game : IGame<State, Action, Player>
    {
        public Game()
        {
        }

        public List<Action> GetActions(State state)
        {
            //Genera un'azione per ogni posizione libera della griglia
            List<Action> result = new List<Action>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (state.GetBoardValue(i, j) == State.EMPTY)
                        result.Add(new Action(i, j));
                }
            }

            return result;
        }

        public State GetInitialState()
        {
            return new State();
        }

        public Player GetPlayer(State state)
        {
            return state.CurrentPlayer;
        }

        public Player[] GetPlayers()
        {
            return new Player[2] { Player.Cross, Player.Circle };
        }

        public State GetResult(State state, Action action)
        {
            //Apply action to the state
            int[,] newBoard = (int[,])state.Board.Clone();
            newBoard[action.Row, action.Col] = (int)state.CurrentPlayer;

            Player player;
            //Switch player
            if (state.CurrentPlayer == Player.Circle)
                player = Player.Cross;
            else
                player = Player.Circle;

            return new State(newBoard, player);
        }

        public double GetUtility(State state, Player player)
        {
            if (HasWon(state, player))
                return double.PositiveInfinity;

            Player otherPlayer;
            if (player == Player.Circle)
                otherPlayer = Player.Cross;
            else
                otherPlayer = Player.Circle;

            if (HasWon(state, otherPlayer))
                return double.NegativeInfinity;

            //Se nessuno dei due ha vinto (quindi se c'è un pareggio o se la partita non è ancora finita)
            return 0;

        }

        public bool HasWon(State state, Player player)
        {
            bool won;

            //Verticale
            for (int col = 0; col < 3; col++)
            {
                won = true;
                for (int row = 0; row < 3; row++)
                {
                    if (state.GetBoardValue(row, col) != (int)player)
                        won = false;
                }
                if (won)
                    return true;
            }
            //Orizzontale
            for (int row = 0; row < 3; row++)
            {
                won = true;
                for (int col = 0; col < 3; col++)
                {
                    if (state.GetBoardValue(row, col) != (int)player)
                        won = false;
                }
                if (won)
                    return true;
            }
            //Diagonale principale
            won = true;
            for (int i = 0; i < 3; i++)
            {
                if (state.GetBoardValue(i, i) != (int)player)
                    won = false;
            }
            if (won)
                return true;
            //Diagonale secondaria
            won = true;
            for (int i = 0; i < 3; i++)
            {
                if (state.GetBoardValue(i, 2 - i) != (int)player)
                    won = false;
            }
            if (won)
                return true;

            //Altrimenti
            return false;
        }

        public bool IsTerminal(State state)
        {
            //Condizione di terminazione: presenza di un tris in verticale, orizzontale o diagonale, oppure griglia piena

            //Controllo verticale
            for (int col = 0; col < 3; col++)
            {
                if (state.GetBoardValue(0, col) == state.GetBoardValue(1, col)
                    && state.GetBoardValue(1, col) == state.GetBoardValue(2, col)
                    && state.GetBoardValue(0, col) != State.EMPTY)
                    return true;
            }

            //Controllo orizzontale
            for (int row = 0; row < 3; row++)
            {
                if (state.GetBoardValue(row, 0) == state.GetBoardValue(row, 1)
                    && state.GetBoardValue(row, 1) == state.GetBoardValue(row, 2)
                    && state.GetBoardValue(row, 0) != State.EMPTY)
                    return true;
            }

            //Controllo diagonale principale
            if (state.GetBoardValue(0, 0) == state.GetBoardValue(1, 1)
                && state.GetBoardValue(1, 1) == state.GetBoardValue(2, 2)
                && state.GetBoardValue(0, 0) != State.EMPTY)
                return true;
            //Controllo diagonale secondaria
            if (state.GetBoardValue(0, 2) == state.GetBoardValue(1, 1)
                && state.GetBoardValue(1, 1) == state.GetBoardValue(2, 0)
                && state.GetBoardValue(0, 2) != State.EMPTY)
                return true;

            //Griglia piena
            bool full = true;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (state.GetBoardValue(row, col) == State.EMPTY)
                        full = false;
                }
            }
            if (full)
                return true;


            //Se non ha trovato nulla, non è terminale
            return false;
        }
    }
}
