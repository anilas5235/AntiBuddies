using System;
using Project.Scripts.Spawning.Pooling;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Project.Behaviors.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "GetPool", story: "Get [Pool] of Type", category: "Action", id: "3c7db8e83071e52b4278284ca7bd948e")]
    public partial class GetPoolAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObjectPool> Pool;
        [SerializeReference] public BlackboardVariable<AvailablePool> Type;

        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            Pool.Value ??= GlobalPools.Instance.GetPoolFor(Type.Value);
            return Status.Success;
        }

        protected override void OnEnd()
        {
        }
    }
}

