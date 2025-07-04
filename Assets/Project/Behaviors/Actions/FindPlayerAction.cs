using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Project.Behaviors.Actions
{
    /// <summary>
    /// Action node that finds the player GameObject in the scene and stores its Transform in the blackboard.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
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

            if (!player)
            {
                Debug.LogWarning("Could not find player in FindPlayerAction");
                return Status.Failure;
            }
            Player.Value = player.GetComponent<Transform>();

            return Status.Success;
        }
    }
}