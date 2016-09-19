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

        /// <summary>
        /// Initializes a new instance of the <see cref="Misuno.State"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        public State (string name = "Unnamed")
        {
            this.Name = name;
        }

        /// <summary>
        /// Method which is callen once when state is entered. 
        /// </summary>
        virtual public void Enter ()
        {
        }

        /// <summary>
        /// Update method. Called every frame.
        /// </summary>
        virtual public void Update ()
        {
        }

        /// <summary>
        /// Method which called once when state is finished.
        /// </summary>
        virtual public void Exit ()
        {
            finished = false;
        }
    }
}