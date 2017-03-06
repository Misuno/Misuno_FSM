using System;

namespace Misuno
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class FSM : State
    {
        private State initialState;

        private Coroutine rootFSMLoopCoroutine;

        private Coroutine transitionBetweenStatesCoroutine;
        private bool isRootFSM;

        public State ActiveState { get; private set; }

        private readonly List<State> states = new List<State>();

        private readonly Dictionary<State, List<StateTransition>> transitions = new Dictionary<State, List<StateTransition>>();

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Misuno.FSM"/> class.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="name">Name.</param>
        public FSM(MonoBehaviour sender, string name)
            : base(sender, name)
        {
        }

        #endregion

        #region Enter Exit Update Check

        public override void Enter()
        {
            base.Enter();
            ActiveState = initialState;
            ActiveState.Enter();

            if (isRootFSM)
            {
                rootFSMLoopCoroutine = StartCoroutine(RootFSMLoop());
            }
        }

        public override void Update()
        {
            if (Finished)
                return;   
            
            CheckTransitions();

            if (transitionBetweenStatesCoroutine != null)
                return;
                
            ActiveState.Update();
        }

        private void CheckTransitions()
        {
            if (ActiveState != null)
            {
                // Check both global transitions and current state's transitions.
                List<StateTransition> stateTransitions = transitions[ActiveState];

                if (stateTransitions.Count > 0)
                {
                    foreach (StateTransition transition in stateTransitions)
                    {
                        if (!transition.Check())
                            continue;

                        transitionBetweenStatesCoroutine = StartCoroutine(TransitionBetweenStates(transition));
                    }
                }
                else
                {
                    Finished = ActiveState.Finished;
                }
            }
        }

        public override IEnumerator Exit()
        {
            if (rootFSMLoopCoroutine != null)
            {
                StopCoroutine(rootFSMLoopCoroutine);
            }

            if (transitionBetweenStatesCoroutine != null)
            {
                StopCoroutine(transitionBetweenStatesCoroutine);
            }

            yield return StartCoroutine(base.Exit());
        }

        private IEnumerator TransitionBetweenStates(StateTransition transition)
        {
            // Reset all current state's transtions.
            transitions[ActiveState].ForEach(trans => trans.Reset());

            yield return StartCoroutine(ActiveState.Exit());

            // Switch state.
            ActiveState = transition.toState;
            ActiveState.Enter();

            // Wait one more frame to prevent calling an Update at the same tick.
            yield return null;

            transitionBetweenStatesCoroutine = null;
        }

        private IEnumerator RootFSMLoop()
        {
            while (!Finished)
            {
                Update();
                yield return null;
            }

            StartCoroutine(Exit());
        }

        #endregion

        #region Utility methods

        public void Start()
        {
            isRootFSM = true;
            Enter();
        }

        /// <summary>
        /// Adds the state to the FSM.
        /// </summary>
        /// <param name="state">State to add.</param>
        /// <param name="setInitial">If set to <c>true</c> sets the state as initial.</param>
        public void AddState(State state, bool setInitial = false)
        {
            if (!states.Contains(state))
            {
                states.Add(state);
                transitions[state] = new List<StateTransition>();
            }

            if (setInitial)
            {
                SetInitialState(state);
            }
        }

        /// <summary>
        /// Adds the transition to the FSM.
        /// </summary>
        /// <param name="transition">Transaction to add.</param>
        public void AddTransition(StateTransition transition)
        {
            if (states.Contains(transition.fromState) && states.Contains(transition.toState))
            {
                transitions[transition.fromState].Add(transition);
            }
        }

        private void SetInitialState(State state)
        {
            initialState = state;
        }

        #endregion

        public override string ToString()
        {
            string activeStateName = ActiveState == null ? "null" : ActiveState.ToString();
            return string.Format("[{0}->[{1}]]", name, activeStateName);
        }

        public override Vector2 Draw(Vector2 position)
        {
            
            // Draw state.

            // Dtaw all states, conected to initial.


            return position;
        }

    }
}