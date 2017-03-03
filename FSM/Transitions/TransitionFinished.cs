namespace Misuno
{
    public class TransitionFinished : StateTransition
    {
        public TransitionFinished (State fromState, State toState) :
            base (fromState, toState)
        {
        }

        public override bool Check ()
        {
            return fromState.Finished;
        }
    }
}