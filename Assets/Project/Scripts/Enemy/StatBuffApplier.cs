using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class StatBuffApplier : MonoBehaviour, IHandleContact
    {
        [SerializeField] private StatBuffData statBuffData;
        [SerializeField] private AllyGroup allyGroup = AllyGroup.Enemy;

        public void HandleContact(GameObject contact)
        {
            ContactToHubAdapter hubAdapter = new(contact, allyGroup);
            if (!hubAdapter.IsValid) return;
            hubAdapter.Apply(statBuffData?.GetBuff(null));
        }
    }
}