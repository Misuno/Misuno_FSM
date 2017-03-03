using System.Collections;
using UnityEngine;

namespace Misuno
{
    public abstract class State
    {
        public readonly string name;
        protected readonly MonoBehaviour host;

        private MonoBehaviour worker;

        protected MonoBehaviour Worker
        {
            get
            {
                if (worker == null)
                {
                    worker = host.GetComponent<FSMWorker>();
                    if (worker == null)
                    {
                        worker = host.gameObject.AddComponent<FSMWorker>();
                    }
                }
                return worker;
            }
        }

        public bool Finished { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Misuno.State"/> class.
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name="name">Name.</param>
        public State(MonoBehaviour sender, string name)
        {
            this.name = name;
            host = sender;
        }

        /// <summary>
        /// Method which is callen once when state is entered. 
        /// </summary>
        public virtual void Enter()
        {
            Finished = false;
//            Debug.Log(ToString() + " ENTER()");
        }

        /// <summary>
        /// Update method. Called every frame.
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// Method which called once when state is finished.
        /// </summary>
        public virtual IEnumerator Exit()
        {
            yield break;
        }

        public void Clear()
        {
            if (worker != null)
            {
                worker.StopAllCoroutines();
            }
        }

        protected Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return Worker.StartCoroutine(coroutine);
        }

        protected void StopCoroutine(Coroutine coroutine)
        {
            Worker.StopCoroutine(coroutine);
        }

        public override string ToString()
        {
            return string.Format("[State: {0}]", name);
        }
    }
}