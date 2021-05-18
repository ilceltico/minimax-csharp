using System;
using minimax.core.framework;

namespace minimax.core.adversarial
{
    /**
     * <summary>
     * Artificial Intelligence A Modern Approach (3rd Edition): page 169.
     * 
     * @author Federico Stella
     * </summary>
     */
    public class MinimaxSearch<S, A, P> : AdversarialSearch<S, A>
    {
		public readonly static String METRICS_NODES_EXPANDED = "nodesExpanded";

		private IGame<S, A, P> game;
		private Metrics metrics = new Metrics();

		/**
		 * Creates a new search object for a given game.
		 */
		public static MinimaxSearch<S, A, P> createFor(IGame<S, A, P> game)
		{
			return new MinimaxSearch<S, A, P>(game);
		}

		public MinimaxSearch(IGame<S, A, P> game)
		{
			this.game = game;
		}

		public A makeDecision(S state)
		{
			metrics = new Metrics();
			metrics.Set(METRICS_NODES_EXPANDED, 0);

			A result = default;
			double resultValue = double.NegativeInfinity;
			P player = game.GetPlayer(state);
			foreach (A action in game.GetActions(state))
			{
				double newValue = minValue(game.GetResult(state, action), player);
				if (newValue > resultValue)
				{
					result = action;
					resultValue = newValue;
				}
			}
			return result;
		}


		public double maxValue(S state, P player)
		{ // returns a utility value
			metrics.IncrementInt(METRICS_NODES_EXPANDED);
			if (game.IsTerminal(state))
				return game.GetUtility(state, player);

			double v = double.NegativeInfinity;
			foreach (A action in game.GetActions(state))
            {
				S newState = game.GetResult(state, action);
				double newValue = minValue(newState, player);
				if (newValue > v)
					v = newValue;
            }

			return v;
		}


		public double minValue(S state, P player)
		{ // returns a utility value
			metrics.IncrementInt(METRICS_NODES_EXPANDED);
			if (game.IsTerminal(state))
				return game.GetUtility(state, player);

			double v = double.PositiveInfinity;
			foreach (A action in game.GetActions(state))
            {
				S newState = game.GetResult(state, action);
				double newValue = maxValue(newState, player);
				if (newValue < v)
					v = newValue;
            }

			return v;
		}

		public Metrics getMetrics()
		{
			return metrics;
		}
	}
}
