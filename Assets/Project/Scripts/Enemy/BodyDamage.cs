using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.StatSystem;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class BodyDamage : MonoBehaviour, IHandleContact, INeedStatComponent
    {
        [SerializeField] private DamageDefinition damage = new();
        [SerializeField] private DamageBuffData buffData;
        [SerializeField] private AllyGroup allyGroup = AllyGroup.Enemy;
        private StatComponent _statComponent;

        public void HandleContact(GameObject contact)
        {
            ContactToHubAdapter hubAdapter = new(contact, allyGroup);
            if (!hubAdapter.IsValid) return;
            hubAdapter.Apply(damage.CreatePackage(gameObject, _statComponent));
            hubAdapter.Apply(buffData?.GetBuff(null, gameObject, _statComponent));
        }

        public void OnStatInit(StatComponent statComponent)
        {
            _statComponent = statComponent;
        }
    }
}