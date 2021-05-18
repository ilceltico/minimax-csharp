using System;
using System.Collections.Generic;

namespace minimax.core.adversarial
{
    /**
     * <summary>
     * <para>Artificial Intelligence A Modern Approach (3rd Edition): page 165.</para>
     * 
     * A game can be formally defined as a kind of search problem with the following
     * elements:
     * <list type="bullet">
     * <item>S0: The initial state, which specifies how the game is set up at the
     * start.</item>
     * <item>PLAYER(s): Defines which player has the move in a state.</item>
     * <item>ACTIONS(s): Returns the set of legal moves in a state.</item>
     * <item>RESULT(s, a): The transition model, which defines the result of a move.</item>
     * <item>TERMINAL-TEST(s): A terminal test, which is true when the game is over
     * and false TERMINAL STATES otherwise. States where the game has ended are
     * called terminal states.</item>
     * <item>UTILITY(s, p): A utility function (also called an objective function or
     * payoff function), defines the final numeric value for a game that ends in
     * terminal state s for a player p. In chess, the outcome is a win, loss, or
     * draw, with values +1, 0, or 1/2 . Some games have a wider variety of possible
     * outcomes; the payoffs in backgammon range from 0 to +192. A zero-sum game is
     * (confusingly) defined as one where the total payoff to all players is the
     * same for every instance of the game. Chess is zero-sum because every game has
     * payoff of either 0 + 1, 1 + 0 or 1/2 + 1/2 . "Constant-sum" would have been a
     * better term, but zero-sum is traditional and makes sense if you imagine each
     * player is charged an entry fee of 1/2.</item>
     * </list>
     * 
     * @author Federico Stella
     * </summary>
     */
    public interface IGame<S, A, P>
    {

        /// <summary>
        /// Get the initial state of the game
        /// </summary>
        /// <returns></returns>
        S GetInitialState();

        /// <summary>
        /// Get the players
        /// </summary>
        /// <returns></returns>
        P[] GetPlayers();

        /// <summary>
        /// Get the current player
        /// </summary>
        /// <param name="state">Current game state</param>
        /// <returns></returns>
        P GetPlayer(S state);

        /// <summary>
        /// Get the list of legal actions from the current state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        List<A> GetActions(S state);

        /// <summary>
        /// Applies an action to a state and returns the resulting state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        S GetResult(S state, A action);

        /// <summary>
        /// Returns true if the state is a terminal state for the game
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        bool IsTerminal(S state);

        /// <summary>
        /// Returns the value of the state for the specified player. A positive value means a favorable state.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        double GetUtility(S state, P player);
    }
}
