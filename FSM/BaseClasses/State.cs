using UnityEngine;
using System.Collections;

namespace Misuno
{
    public class State
    {
        public readonly string Name;

        protected bool finished = false;

        public bool Finished {
            get { return finished; }
        }

        public State (string name = "Unnamed")
        {
            this.Name = name;
        }

        virtual public void Enter ()
        {
        }

        virtual public void Update ()
        {
        }

        virtual public void Exit ()
        {
            finished = false;
        }
    }
}