using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Random = UnityEngine.Random;

namespace Project.Behaviors.Actions
{
    /// <summary>
    /// Action node that generates a random position within a specified radius and clamps it inside a rectangle.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "PositionInRect", story: "Random 2DPos [Position] in Rect", category: "Action",
        id: "b15dbb724ccdea7c7ff04699fc35917e")]
    public partial class PositionInRectAction : Action
    {
        [SerializeReference] public BlackboardVariable<Vector2> Position;
        [SerializeReference] public BlackboardVariable<Vector2> Size = new(Vector2.one);
        [SerializeReference] public BlackboardVariable<Transform> Transform;
        [SerializeReference] public BlackboardVariable<float> Radius = new(1f);

        /// <summary>
        /// Always returns Status.Running to indicate the action is in progress.
        /// </summary>
        protected override Status OnStart()
        {
            return Status.Running;
        }

        /// <summary>
        /// Generates a random position within a circle of given radius around the Transform,
        /// then clamps it to the rectangle defined by Size.
        /// </summary>
        protected override Status OnUpdate()
        {
            Vector2 rect = Size.Value;
            float halfWidth = rect.x / 2;
            Position.Value = (Vector2)Transform.Value.position + (Random.insideUnitCircle * Radius);
            // Clamp the position to stay within the rectangle bounds
            Position.Value = new Vector2(
                Mathf.Clamp(Position.Value.x, -halfWidth, halfWidth),
                Mathf.Clamp(Position.Value.y, -halfWidth, halfWidth)
            );
            return Status.Success;
        }

        /// <summary>
        /// No cleanup required for this action.
        /// </summary>
        protected override void OnEnd()
        {
        }
    }
}