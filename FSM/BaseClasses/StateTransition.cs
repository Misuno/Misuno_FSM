namespace Misuno
{
    public abstract class StateTransition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Misuno.StateTransition"/> class.
        /// </summary>
        /// <param name="from">State from.</param>
        /// <param name="to">State to.</param>
        public StateTransition (State from, State to)
        {
            From = from;
            To = to;
        }

        public readonly State From;
        public readonly State To;


        abstract public bool Check ();
    }
}