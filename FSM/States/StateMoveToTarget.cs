
namespace Misuno
{
    using UnityEngine;

    public class StateMoveToTarget : State
    {
        public Transform Target;
        public float rotationSpeed;
        public float speed;
        public float acceleration;
        public float minDistance;

        readonly IHasTarget host;
        Movement mover;

        public StateMoveToTarget (IHasTarget sender, string name, float dist, float speed = 200f, float rotationSpeed = 3f, float acceleration = 100f) :
            base (name)
        {
            this.host = sender;
            minDistance = dist;
            this.speed = speed;
            this.rotationSpeed = rotationSpeed;
            this.acceleration = acceleration;
            mover = sender.gameObject.GetComponent<Movement> ();
        }

        override public void Enter ()
        {
            Target = host.Target;
        }

        public override void Update ()
        {
            if (!finished && Target != null)
            {
                var dist = Vector3.Distance (host.transform.position, Target.transform.position);
                if (dist > minDistance)
                {
                    Debug.DrawLine (host.transform.position, Target.transform.position, Color.green);

                    var direction = Target.transform.position - host.transform.position;
                    mover.RotateTowards (host.transform, direction, rotationSpeed);

                    var rb2d = host.gameObject.GetComponent<Rigidbody2D> ();
                    mover.MoveForward (rb2d, host.transform.right, acceleration, speed);

                    return;
                }
            }
            finished = true;
        }
    }

    public interface IHasTarget
    {
        Transform Target { get; }

        Transform transform { get; }

        GameObject gameObject { get; }
    }
}

