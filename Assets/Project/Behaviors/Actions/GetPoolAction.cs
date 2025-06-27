using System;
using Project.Scripts.Spawning.Pooling;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Project.Behaviors.Actions
{
    /// <summary>
    /// Action node that retrieves a GameObjectPool for a given PoolObject and stores it in the blackboard.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "GetPool", story: "Get [Pool] for [PoolObject]", category: "Action",
        id: "3c7db8e83071e52b4278284ca7bd948e")]
    public partial class GetPoolAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObjectPool> Pool;
        [SerializeReference] public BlackboardVariable<GameObject> PoolObject;

        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            Pool.Value ??= GlobalPools.Instance.GetPoolFor(PoolObject.Value);
            return Status.Success;
        }

        protected override void OnEnd()
        {
        }
    }
}