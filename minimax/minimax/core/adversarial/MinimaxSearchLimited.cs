using System;
using minimax.core.framework;

namespace minimax.core.adversarial
{
	/**
     * <summary>
     * Artificial Intelligence A Modern Approach (3rd Edition): page 169.
     * Addition of a depth parameter to handle iterative deepening approaches.
     * 
     * @author Federico Stella
     * </summary>
     */
	public class MinimaxSearchLimited<S, A, P> : AdversarialSearch<S, A>
	{
		public readonly static String METRICS_NODES_EXPANDED = "nodesExpanded";

		private IGame<S, A, P> game;
		private int maxDepth;
		private Metrics metrics = new Metrics();
		private bool fullyExplored;


		/// <summary>
        /// A maxDepth value of 1 will only consider the actions of the current player.
        /// A maxDepth value of 2 will also consider the ones from its opponent.
        /// A maxDepth value of 0 will search the whole tree.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="maxDepth">Maximum depth of the tree, non-negative</param>
		public MinimaxSearchLimited(IGame<S, A, P> game, int maxDepth)
		{
			this.game = game;
			if (maxDepth < 0)
				throw new ArgumentOutOfRangeException("MaxDepth should be non-negative");
			this.maxDepth = maxDepth;
		}

		/// <summary>
        /// Creates a Minimax instance with unlimited depth.
        /// </summary>
        /// <param name="game"></param>
		public MinimaxSearchLimited(IGame<S, A, P> game)
		{
			this.game = game;
			this.maxDepth = 0;
		}

		public A makeDecision(S state)
		{
			metrics = new Metrics();
			metrics.Set(METRICS_NODES_EXPANDED, 0);

			fullyExplored = true;

			A result = game.GetActions(state)[0];
			double resultValue = double.NegativeInfinity;
			P player = game.GetPlayer(state);
			foreach (A action in game.GetActions(state))
			{
				double newValue = minValue(game.GetResult(state, action), player, maxDepth-1);
				if (newValue > resultValue)
				{
					result = action;
					resultValue = newValue;
				}
			}
			return result;
		}


		public double maxValue(S state, P player, int limit)
		{ // returns a utility value
			metrics.IncrementInt(METRICS_NODES_EXPANDED);
			if (game.IsTerminal(state))
				return game.GetUtility(state, player);
			if (limit == 0)
            {
				fullyExplored = false;
				return game.GetUtility(state, player);
            }

			double v = double.NegativeInfinity;
			foreach (A action in game.GetActions(state))
			{
				S newState = game.GetResult(state, action);
				double newValue = minValue(newState, player, limit-1);
				if (newValue > v)
					v = newValue;
			}

			return v;
		}


		public double minValue(S state, P player, int limit)
		{ // returns a utility value
			metrics.IncrementInt(METRICS_NODES_EXPANDED);
			if (game.IsTerminal(state))
				return game.GetUtility(state, player);
			if (limit == 0)
			{
				fullyExplored = false;
				return game.GetUtility(state, player);
			}

			double v = double.PositiveInfinity;
			foreach (A action in game.GetActions(state))
			{
				S newState = game.GetResult(state, action);
				double newValue = maxValue(newState, player, limit-1);
				if (newValue < v)
					v = newValue;
			}

			return v;
		}

		public Metrics getMetrics()
		{
			return metrics;
		}

		public bool FullyExplored
        {
			get { return fullyExplored; }
        }
	}
}
