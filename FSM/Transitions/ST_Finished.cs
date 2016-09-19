namespace Misuno
{
    public class ST_Finished : StateTransition
    {
        public ST_Finished (State from, State to) :
            base (from, to)
        {
        }

        override public bool Check ()
        {
            return From.Finished;
        }
    }
}