using UnityEngine;

namespace Misuno
{

    public class TransitionTargetDestroyed : StateTransition
    {
        public IHasTarget host;

        public TransitionTargetDestroyed (State from, State to, IHasTarget target) :
            base (from, to)
        {
            host = target;
        }

        public override bool Check ()
        {
            return host.Target == null;
        }
    }
}
