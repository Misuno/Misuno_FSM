using UnityEngine;

namespace Misuno
{
    public class StateMoveToPosition : State
    {
        public readonly Vector3 destination;
        public float rotationSpeed = 0.1f;
        public float speed = 10f;
        public GameObject host;

        public StateMoveToPosition (GameObject sender, string name, Vector3 destination, float speed = 10f, float rotationSpeed = 0.1f) :
            base (name)
        {
            host = sender;
            this.destination = destination;
            this.speed = speed;
            this.rotationSpeed = rotationSpeed;
        }

        public override void Update ()
        {
            if (!finished)
            {
                var distance = Vector3.Distance (host.transform.position, destination);
                if (distance > 1f)
                {
                    var direction = destination - host.transform.position;
                    direction.Normalize ();
                    host.transform.rotation = Quaternion.Slerp (host.transform.rotation, Quaternion.LookRotation (direction), rotationSpeed);
                    host.transform.Translate (Vector3.forward * Time.deltaTime * speed);
                }
                else
                {
                    finished = true;
                }
            }
        }
    }
}

