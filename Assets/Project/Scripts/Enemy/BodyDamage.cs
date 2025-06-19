using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.StatSystem;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class BodyDamage : MonoBehaviour, IHandleContact, INeedStatGroup
    {
        [SerializeField] private DamageDefinition damage = new();
        [SerializeField] private DamageBuffData buffData;
        [SerializeField] private AllyGroup allyGroup = AllyGroup.Enemy;
        private IStatGroup _statGroup;

        public void HandleContact(GameObject contact)
        {
            ContactToHubAdapter hubAdapter = new(contact, allyGroup);
            if (!hubAdapter.IsValid) return;
            hubAdapter.Apply(damage.CreatePackage(gameObject, _statGroup));
            hubAdapter.Apply(buffData?.GetBuff(null, gameObject, _statGroup));
        }

        public void OnStatInit(IStatGroup statGroup)
        {
            _statGroup = statGroup;
        }
    }
}