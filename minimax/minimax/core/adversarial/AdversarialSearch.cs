using System;
using minimax.core.framework;

namespace minimax.core.adversarial
{
    /// <summary>
    /// C# porting of aima's AdversarialSearch
    /// Variant of the search interface. Since players can only control the next
    /// move, method <c>makeDecision</c> returns only one action, not a
    /// sequence of actions.
    ///
    /// @author Federico Stella
    /// </summary>
    /// <typeparam name="S">The type used to represent states</typeparam>
    /// <typeparam name="A">The type of the actions to be used to navigate through the state space</typeparam>
    public interface AdversarialSearch<S, A>
    {

        /// <summary>
        /// Returns the action which appears to be the best at the given state.
        /// </summary>
        /// <param name="state">The current game state</param>
        /// <returns></returns>
        A makeDecision(S state);

        /// <summary>
        /// Returns all the metrics of the search.
        /// </summary>
        /// <returns></returns>
        Metrics getMetrics();
    }
}
