using System;
using Project.Scripts.Spawning.Pooling;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Project.Behaviors.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "PoolSpawn", story: "Gets an GameObject from a [Pool]", category: "Action",
        id: "38c01513dde06b82dd600b41da14ee4a")]
    public partial class PoolSpawnAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObjectPool> Pool;
        [SerializeReference] public BlackboardVariable<Transform> Transform;
        [SerializeReference] public BlackboardVariable<GameObject> ResultGameObject;

        protected override Status OnStart()
        {
            if (!Pool.Value)
            {
                Debug.LogError("No Pool found");
                return Status.Failure;
            }

            return Status.Running;
        }

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

        protected override void OnEnd()
        {
        }
    }
}