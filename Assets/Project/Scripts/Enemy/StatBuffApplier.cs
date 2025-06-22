using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Enemy
{
    public class StatBuffApplier : MonoBehaviour, IHandleContact
    {
        [SerializeField] private StatBuffData statBuffData;
        [SerializeField] private AlliedGroup alliedGroup = AlliedGroup.Enemy;

        public void HandleContact(GameObject contact)
        {
            ContactToHubAdapter hubAdapter = new(contact, alliedGroup);
            if (!hubAdapter.IsValid) return;
            hubAdapter.Apply(statBuffData?.GetBuff(null));
        }
    }
}