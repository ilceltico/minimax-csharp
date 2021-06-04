using System;
using minimax.core.adversarial;

namespace minimax.tictactoe
{
    class Program
    {
        static void Main(string[] args)
        {
            int maximumThinkingMilliseconds = -1; //Set to -1 to have it unlimited

            Console.WriteLine("Tic-Tac-Toe Artificial Intelligence\n");

            Game game = new Game();
            State state = game.GetInitialState();

            AdversarialSearch<State, tictactoe.Action> minimaxSearch = new MinimaxSearch<State, tictactoe.Action, Player>(game);
            //AdversarialSearch<State, tictactoe.Action> minimaxSearch = new MinimaxSearchLimited<State, tictactoe.Action, Player>(game, 2);
            //AdversarialSearch<State, tictactoe.Action> minimaxSearch = new IterativeDeepening<State, tictactoe.Action, Player>(game, IterativeDeepening<State, tictactoe.Action, Player>.Algorithm.Minimax);
            //AdversarialSearch<State, tictactoe.Action> minimaxSearch =
            //    new IterativeDeepening<State, tictactoe.Action, Player>(game, IterativeDeepening<State, tictactoe.Action, Player>.Algorithm.Minimax, maximumThinkingMilliseconds);

            String input;
            bool first;
            do
            {
                Console.Write("Do you want to play first? (y/n): ");
                input = Console.ReadLine();
            } while (input.ToLower() != "y" && input.ToLower() != "n");
            first = input.ToLower() == "y";

            Player humanPlayer = first ? Player.Cross : Player.Circle;
            String humanPlayerString = first ? "X" : "O";


            while (game.IsTerminal(state) == false)
            {
                if (first)
                {
                    Console.WriteLine("\n" + state.ToString());
                    Console.Write($"You are {humanPlayerString}, select an action (02 means row=0, col=2): ");
                    input = Console.ReadLine();
                    int row = Convert.ToInt32(input.Substring(0, 1));
                    int col = Convert.ToInt32(input.Substring(1, 1));

                    state = game.GetResult(state, new tictactoe.Action(row, col));

                    if (game.IsTerminal(state))
                        break;
                }
                first = true;

                Console.WriteLine("\n" + state.ToString());
                Console.WriteLine("AI is thinking about its next move...");


                var watch = System.Diagnostics.Stopwatch.StartNew();
                tictactoe.Action action = minimaxSearch.makeDecision(state);
                watch.Stop();

                Console.WriteLine($"Selected action: Row={action.Row}, Col={action.Col}");
                Console.WriteLine($"Expanded nodes: {minimaxSearch.getMetrics().Get(MinimaxSearch<State, tictactoe.Action, Player>.METRICS_NODES_EXPANDED)}");
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms\n");


                state = game.GetResult(state, action);
            }

            Console.WriteLine("\n" + state.ToString());

            if (game.HasWon(state, humanPlayer))
            {
                Console.WriteLine("\nYOU WON, CONGRATULATIONS!!!");
            }
            else if (game.IsTerminal(state))
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
