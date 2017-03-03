namespace Misuno
{
    public abstract class StateTransition
    {
        public readonly State fromState;
        public readonly State toState;

        /// <summary>
        /// Initializes a new instance of the <see cref="Misuno.StateTransition"/> class.
        /// </summary>
        /// <param name="fromState">State from.</param>
        /// <param name="toState">State to.</param>
        protected StateTransition(State fromState, State toState)
        {
            this.fromState = fromState;
            this.toState = toState;
        }

        protected StateTransition()
        {
        }

        public abstract bool Check();

        public virtual void Reset()
        {
        }

        public virtual void Clear()
        {
        }
    }
}