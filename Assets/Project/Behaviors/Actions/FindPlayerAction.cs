using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Project.Behaviors.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Find Player", story: "Find [Player]", category: "Action",
        id: "66b8e655f5894e681cc0d37fef049c24")]
    public partial class FindPlayerAction : Action
    {
        [SerializeReference] public BlackboardVariable<Transform> Player;

        protected override Status OnStart()
        {
            if (Player.Value) return Status.Success;
            // Find the player in the scene
            GameObject player = GameObject.FindWithTag("Player"); // Find the player by tag
            Debug.Log($"{player} is at {player.transform.position}");

            if (!player) return Status.Failure;
            Player.Value = player.GetComponent<Transform>();

            return Status.Success;
        }
    }
}