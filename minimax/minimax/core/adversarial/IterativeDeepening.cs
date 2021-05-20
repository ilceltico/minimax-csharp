using System;
using System.Threading.Tasks;
using minimax.core.framework;

namespace minimax.core.adversarial
{
    /// <summary>
    /// A class that handles iterative deepening approaches for adversarial searches.
    ///
    /// @author Federico Stella
    /// </summary>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="P"></typeparam>
    public class IterativeDeepening<S, A, P> : AdversarialSearch<S, A>
    {
        public enum Algorithm
        {
            Minimax
        }

        private AdversarialSearch<S, A> search;
        private Algorithm algorithm;
        private IGame<S, A, P> game;
        private int timeout;


        /// <summary>
        /// New IterativeDeepening algorithm with the specified algorithm and time limit.
        /// When the time limit is reached the computation stops and the last best action is returned.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="algorithm">Adversarial search algorithm to be used</param>
        /// <param name="timeout">Maximum execution time in milliseconds</param>
        public IterativeDeepening(IGame<S, A, P> game, Algorithm algorithm, int timeout)
        {
            switch (algorithm)
            {
                case Algorithm.Minimax:
                    this.search = new MinimaxSearchLimited<S, A, P>(game, 1);
                    break;
                default:
                    throw new ArgumentException("Invalid algorithm");
            }

            this.algorithm = algorithm;
            this.game = game;
            this.timeout = timeout;
        }

        /// <summary>
        /// New IterativeDeepening algorithm with no time limits
        /// </summary>
        /// <param name="game"></param>
        /// <param name="algorithm">Adversarial search algorithm to be used</param>
        public IterativeDeepening(IGame<S, A, P> game, Algorithm algorithm)
        {
            switch (algorithm)
            {
                case Algorithm.Minimax:
                    this.search = new MinimaxSearchLimited<S, A, P>(game, 1);
                    break;
                default:
                    throw new ArgumentException("Invalid algorithm");
            }

            this.algorithm = algorithm;
            this.game = game;
            this.timeout = -1; //Unlimited wait time
        }

        public Metrics getMetrics()
        {
            return search.getMetrics();
        }

        public A makeDecision(S state)
        {
            int level = 1;
            A action = default;
            int currentTimeout = this.timeout;
            while (true)
            {
                switch (algorithm)
                {
                    case Algorithm.Minimax:
                        search = new MinimaxSearchLimited<S, A, P>(game, level);
                        break;
                }

                A selectedGameAction = default;
                void taskAction() { selectedGameAction = search.makeDecision(state); }
                Task task = new Task(taskAction);

                var watch = System.Diagnostics.Stopwatch.StartNew();
                task.Start();
                bool finishedExecution = task.Wait(currentTimeout);
                watch.Stop();

                if (finishedExecution)
                {
                    String expandedNodes = "";
                    bool fullyExplored = false;
                    switch (algorithm)
                    {
                        case Algorithm.Minimax:
                            expandedNodes = MinimaxSearchLimited<S, A, P>.METRICS_NODES_EXPANDED;
                            fullyExplored = ((MinimaxSearchLimited<S, A, P>)search).FullyExplored;
                            break;
                    }

                    action = selectedGameAction;
                    Console.WriteLine($"Level {level} completed");
                    Console.WriteLine($"Selected action: {selectedGameAction}");
                    Console.WriteLine($"Expanded nodes: {search.getMetrics().Get(expandedNodes)}");
                    Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms\n");

                    
                    if (fullyExplored)
                    {
                        Console.WriteLine("The tree has been fully explored.\n");
                        break;
                    }
                }
                else
                    break;
                    
                if (currentTimeout > 0)
                {
                    currentTimeout = currentTimeout - (int)watch.ElapsedMilliseconds;
                    if (currentTimeout <= 0 || !finishedExecution)
                        break;
                }

                level += 1;
            }

            Console.WriteLine($"Execution stopped at level {level}");

            return action;
        }
    }
}
