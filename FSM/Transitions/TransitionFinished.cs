namespace Misuno
{
    public class TransitionFinished : StateTransition
    {
        public TransitionFinished (State from, State to) :
            base (from, to)
        {
        }

        override public bool Check ()
        {
            return From.Finished;
        }
    }
}