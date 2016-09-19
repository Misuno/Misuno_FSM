namespace Misuno
{
    public abstract class StateTransition
    {
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