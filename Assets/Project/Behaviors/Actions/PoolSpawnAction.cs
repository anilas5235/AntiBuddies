using System;
using Project.Scripts.Spawning.Pooling;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Project.Behaviors.Actions
{
    /// <summary>
    /// Action node that spawns an object from a pool and sets its transform if provided.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "PoolSpawn", story: "Gets an GameObject from a [Pool]", category: "Action",
        id: "38c01513dde06b82dd600b41da14ee4a")]
    public partial class PoolSpawnAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObjectPool> Pool;
        [SerializeReference] public BlackboardVariable<Transform> Transform;
        [SerializeReference] public BlackboardVariable<GameObject> ResultGameObject;

        /// <summary>
        /// Checks if the pool is available before spawning.
        /// </summary>
        protected override Status OnStart()
        {
            if (!Pool.Value)
            {
                Debug.LogError("No Pool found");
                return Status.Failure;
            }

            return Status.Running;
        }

        /// <summary>
        /// Spawns an object from the pool, sets its transform if provided, and stores the result in the blackboard.
        /// </summary>
        protected override Status OnUpdate()
        {
            IPoolable obj = Pool.Value.GetObject();
            if (obj == null)
            {
                Debug.LogError("No object found in pool");
                return Status.Failure;
            }

            if (Transform.Value)
            {
                obj.SetTransform(Transform.Value.position, Transform.Value.rotation);
            }

            ResultGameObject.Value = obj.GetGameObject();
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