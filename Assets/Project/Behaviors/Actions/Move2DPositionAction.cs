using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Project.Behaviors.Actions
{
    /// <summary>
    /// Action node that moves a 2D GameObject towards a target position at a specified speed.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Move2DPosition", story: "[Target] 2D Move To [Position]", category: "Action",
        id: "30d8f82469548575ac4fb470969915df")]
    public partial class Move2DPositionAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObject> Target;
        [SerializeReference] public BlackboardVariable<Vector2> Position;
        [SerializeReference] public BlackboardVariable<float> Speed = new(1.0f);
        [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new(0.1f);

        private Rigidbody2D _rigidbody2D;

        protected override Status OnStart()
        {
            // Initializes the movement by checking for required components and if the destination is already reached.
            if (!Target.Value)
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
            if (_rigidbody2D) SetVelocity(Vector2.zero);
        }

        /// <summary>
        /// Calculates the normalized direction vector from the target to the destination.
        /// </summary>
        private Vector2 GetMovementVector()
        {
            return (Position.Value - (Vector2)Target.Value.transform.position).normalized;
        }

        /// <summary>
        /// Checks if the target is within the distance threshold of the destination.
        /// </summary>
        private bool IsDestinationReached()
        {
            return Vector2.Distance(Target.Value.transform.position, Position.Value)
                   <= DistanceThreshold.Value;
        }

        /// <summary>
        /// Sets the Rigidbody2D's linear velocity.
        /// </summary>
        private void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.linearVelocity = velocity;
        }
    }
}