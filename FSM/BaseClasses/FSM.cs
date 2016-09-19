namespace Misuno
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class FSM : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Misuno.FSM"/> class.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="name">Name.</param>
        public FSM (GameObject sender, string name) :
            base (name)
        {
        }

        State initialState;

        public State ActiveState {
            get
            {
                return activeState;
            }
        }

        State activeState;

        readonly List<State> states = new List<State> ();

        readonly Dictionary<State, List<StateTransition>> transactions = new Dictionary<State, List<StateTransition>> ();

        override public void Enter ()
        {
            activeState = initialState;
            activeState.Enter ();
        }

        override public void Update ()
        {
            activeState.Update ();
            var stateTransactions = transactions [activeState];
            if (stateTransactions.Count > 0)
            {
                foreach (var trans in stateTransactions)
                {
                    if (trans.Check ())
                    {
                        activeState.Exit ();
                        activeState = trans.To;
                        activeState.Enter ();
                        break;
                    }
                }
            }
            else
            {
                finished = activeState.Finished;
            }
        }

        override public void Exit ()
        {
        }

        /// <summary>
        /// Adds the state to the FSM.
        /// </summary>
        /// <param name="state">State to add.</param>
        /// <param name="setInitial">If set to <c>true</c> sets the state as initial.</param>
        public void AddState (State state, bool setInitial = false)
        {
            if (!states.Contains (state))
            {
                states.Add (state);
                transactions [state] = new List<StateTransition> ();
            }

            if (setInitial)
            {
                SetInitialState (state);
            }
        }

        void SetInitialState (State state)
        {
            initialState = state;
            Enter ();
        }

        /// <summary>
        /// Adds the transition to the FSM.
        /// </summary>
        /// <param name="transaction">Transaction to add.</param>
        public void AddTransition (StateTransition transaction)
        {
            if (states.Contains (transaction.From) && states.Contains (transaction.To))
            {
                transactions [transaction.From].Add (transaction);
            }
        }
    }
}