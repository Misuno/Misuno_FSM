# Misuno_FSM
# A class-based FSM realisation. 

## Usage:

0) Write some custom States and StateTransitions

1) include namespace Misuno to use it's classes

2) Create a root FSM
```csharp
rootFSM = new FSM (gameObject, name + "_root");
```
3) Create some states and add them to FSM (second parameter of AddState shows that findTarget sould be set as initial)
```csharp
var findTarget = new S_FindNearestTarget (this, "find target", minSearchRadius);
rootFSM.AddState (findTarget, true);

var moveToTarget = new S_MoveToTarget (this, "move to target", 0.01f, moveSpeed, rotationSpeed);
rootFSM.AddState (moveToTarget);
```
4) Add some transitions between states (note, than every state could have several transitions)
```csharp
rootFSM.AddTransition (new ST_Finished (findTarget, moveToTarget));
rootFSM.AddTransition (new ST_TargetDestroyed (moveToTarget, findTarget, this));
rootFSM.AddTransition (new ST_Timer (moveToTarget, findTarget, 15f));
```
5) You could make some complex behaviours by using FSMs as States. 

## Implementing the State

To create their own state one should inherit a new class from a State class. 
Methods to override are:
```csharp
virtual public void Enter ()
virtual public void Update ()
virtual public void Exit ()
```

### Example state

This state is included into the repositiory.

```csharp
using UnityEngine;

namespace Misuno
{
    public class S_MoveToPosition : State
    {
        public readonly Vector3 destination;
        public float rotationSpeed = 0.1f;
        public float speed = 10f;
        public GameObject host;

        public S_MoveToPosition (GameObject sender, string name, Vector3 destination, float speed = 10f, float rotationSpeed = 0.1f) :
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
```