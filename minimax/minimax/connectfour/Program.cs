using System;
using minimax.core.adversarial;

namespace minimax.connectfour
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connect Four Artificial Intelligence\n");

            Game game = new Game(6, 7);
            State state = game.GetInitialState();

            //AdversarialSearch<State, tictactoe.Action> minimaxSearch = new MinimaxSearch<State, tictactoe.Action, Player>(game);
            //AdversarialSearch<State, tictactoe.Action> minimaxSearch = new MinimaxSearchLimited<State, tictactoe.Action, Player>(game, 2);
            AdversarialSearch<State, connectfour.Action> minimaxSearch = new IterativeDeepening<State, connectfour.Action, Player>(game, IterativeDeepening<State, connectfour.Action, Player>.Algorithm.Minimax, 10000);



            while (game.IsTerminal(state) == false)
            {
                Console.WriteLine("\n"+state.ToString());
                Console.Write("You are Blue, select a column (starting from 0): ");
                String input = Console.ReadLine();
                int col = Convert.ToInt32(input.Substring(0, 1));

                //Determinare la riga sapendo la colonna
                int row = -1;
                for (int i = 0; i < state.Rows; i++)
                {
                    if (state.GetBoardValue(i, col) == State.EMPTY)
                    {
                        row = i;
                        break;
                    }
                }

                state = game.GetResult(state, new connectfour.Action(row, col));

                if (game.IsTerminal(state))
                    break;

                Console.WriteLine("\n" + state.ToString());

                Console.WriteLine("AI is thinking about its next move...");


                var watch = System.Diagnostics.Stopwatch.StartNew();
                connectfour.Action action = minimaxSearch.makeDecision(state);
                watch.Stop();

                Console.WriteLine($"Selected action: Col={action.Col}");
                Console.WriteLine($"Expanded nodes: {minimaxSearch.getMetrics().Get(MinimaxSearch<State, tictactoe.Action, Player>.METRICS_NODES_EXPANDED)}");
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms\n");


                state = game.GetResult(state, action);
            }

            Console.WriteLine("\n" + state.ToString());

            if (game.HasWon(state, Player.Blue))
            {
                Console.WriteLine("\nYOU WON, CONGRATULATIONS!!!");
            }
            else if (game.HasWon(state, Player.Red))
            {
                Console.WriteLine("\nAI won, try again!");
            }
            else
            {
                Console.WriteLine("\nIt's a tie, not bad!");
            }


        }
    }
}
