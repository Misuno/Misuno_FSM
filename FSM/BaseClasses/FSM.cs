namespace Misuno
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class FSM : State
    {
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

        public readonly List<State> states = new List<State> ();

        public readonly Dictionary<State, List<StateTransition>> transactions = new Dictionary<State, List<StateTransition>> ();

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

        public void AddTransition (StateTransition trans)
        {
            if (states.Contains (trans.From) && states.Contains (trans.To))
            {
                transactions [trans.From].Add (trans);
            }
        }
    }
}