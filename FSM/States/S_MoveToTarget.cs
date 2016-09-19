
namespace Misuno
{
    using UnityEngine;

    public class S_MoveToTarget : State
    {
        public Transform Target;
        public float rotationSpeed = 0.1f;
        public float speed = 10f;
        public float minDistance;

        readonly IHasTarget host;

        public S_MoveToTarget (IHasTarget sender, string name, float dist, float speed = 10f, float rotationSpeed = 0.1f) :
            base (name)
        {
            this.host = sender;
            minDistance = dist;
            this.speed = speed;
            this.rotationSpeed = rotationSpeed;
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
                    var angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
                    direction.Normalize ();

                    var targetRotation = Quaternion.Euler (new Vector3 (0f, 0f, angle));
                    Quaternion doneRotation;

                    doneRotation = Quaternion.RotateTowards (host.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                    host.transform.rotation = doneRotation;

                    var dx = Mathf.Cos (doneRotation.eulerAngles.z * Mathf.Deg2Rad);
                    var dy = Mathf.Sin (doneRotation.eulerAngles.z * Mathf.Deg2Rad);

                    var targetPos = host.transform.position + new Vector3 (dx, dy, 0f) * speed * Time.deltaTime;
                    var rb2d = host.gameObject.GetComponent<Rigidbody2D> ();
                    rb2d.AddForce (new Vector2 (dx, dy) * speed * Time.deltaTime);
                    rb2d.velocity = rb2d.velocity + (Vector2)(host.transform.forward * speed * 0.5f * Time.deltaTime);
                    rb2d.velocity = Vector2.ClampMagnitude (rb2d.velocity, (host.transform.forward * speed * Time.deltaTime).magnitude);
//                  rb2d.MovePosition (targetPos);
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

