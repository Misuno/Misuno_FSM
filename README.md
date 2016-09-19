# Misuno_FSM
a FSM realisation for Unity3D game engine

A class-based FSM realisation. 

Usage:

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
