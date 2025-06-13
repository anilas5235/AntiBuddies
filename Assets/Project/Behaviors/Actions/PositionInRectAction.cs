using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Random = UnityEngine.Random;

namespace Project.Behaviors.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "PositionInRect", story: "Random 2DPos [Position] in Rect", category: "Action",
        id: "b15dbb724ccdea7c7ff04699fc35917e")]
    public partial class PositionInRectAction : Action
    {
        [SerializeReference] public BlackboardVariable<Vector2> Position;
        [SerializeReference] public BlackboardVariable<Vector2> Size = new(Vector2.one);
        [SerializeReference] public BlackboardVariable<Transform> Transform;
        [SerializeReference] public BlackboardVariable<float> Radius = new(1f);

        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            Vector2 rect = Size.Value;
            float halfWidth = rect.x / 2;
            Position.Value = (Vector2)Transform.Value.position + (Random.insideUnitCircle * Radius);
            Position.Value = new Vector2(
                Mathf.Clamp(Position.Value.x, -halfWidth, halfWidth),
                Mathf.Clamp(Position.Value.y, -halfWidth, halfWidth)
            );
            return Status.Success;
        }

        protected override void OnEnd()
        {
        }
    }
}