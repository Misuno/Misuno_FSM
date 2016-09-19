using UnityEngine;

namespace Misuno
{

    public class ST_TargetDestroyed : StateTransition
    {
        public IHasTarget host;

        public ST_TargetDestroyed (State from, State to, IHasTarget target) :
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
