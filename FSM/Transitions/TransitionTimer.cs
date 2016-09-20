using UnityEngine;

namespace Misuno
{
    public class TransitionTimer: StateTransition
    {
        public readonly float Duration;
        float timer = -1f;

        public TransitionTimer (State from, State to, float duration) :
            base (from, to)
        { 
            Duration = duration;
        }

        override public bool Check ()
        {
            if (timer < 0f)
            {
                timer = Time.time + Duration;
            }

            if (Time.time >= timer)
            {
                timer = -1f;
                return true;
            }
            return false;
        }
    }
}