using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Project.Behaviors.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Move2D To", story: "[Target] 2D Move To [Destination]", category: "Action",
        id: "54a8cc1cd6c83caeb14c7efd5ecfbea9")]
    public partial class Move2DToAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObject> Target;
        [SerializeReference] public BlackboardVariable<GameObject> Destination;
        [SerializeReference] public BlackboardVariable<float> Speed = new(1.0f);
        [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new(0.1f);

        private Rigidbody2D _rigidbody2D;

        protected override Status OnStart()
        {
            if(!Target.Value || !Destination.Value)
            {
                return Status.Failure;
            }
            
            if (IsDestinationReached())
            {
                return Status.Success;
            }

            _rigidbody2D = Target.Value.GetComponent<Rigidbody2D>();
            if (!_rigidbody2D)
            {
                Debug.LogError("No Rigidbody2D found on target");
                return Status.Failure;
            }

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (IsDestinationReached()) return Status.Success;

            SetVelocity(GetMovementVector() * Speed.Value);
            return Status.Running;
        }

        protected override void OnEnd()
        {
            SetVelocity(Vector2.zero);
        }

        private Vector2 GetMovementVector()
        {
            return (Destination.Value.transform.position - Target.Value.transform.position).normalized;
        }

        private bool IsDestinationReached()
        {
            return Vector2.Distance(Target.Value.transform.position, Destination.Value.transform.position) 
                   <= DistanceThreshold.Value;
        }

        private void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.linearVelocity = velocity;
        }
    }
}