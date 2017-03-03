using UnityEngine;

namespace Misuno
{
    public class TransitionTimer : StateTransition
    {
        public readonly float Duration;
        private float timer = -1f;

        public TransitionTimer(State fromState, State toState, float duration) :
            base(fromState, toState)
        {
            Duration = duration;
        }

        public override bool Check()
        {
            if (timer < 0f)
            {
                timer = Time.time + Duration;
            }

            if (Time.time < timer) return false;

            timer = -1f;
            return true;
        }

        public override void Reset()
        {
            base.Reset();
            timer = -1f;
        }
    }
}
