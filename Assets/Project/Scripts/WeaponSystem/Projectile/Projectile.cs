using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : PoolableMono, IProjectile
    {
        [SerializeField] private ProjectileData data;
        [SerializeField] private float speed = 10f;
        [SerializeField] private int allowedContacts = 1;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rb;

        private DamagePackage _damagePackage;
        private DamageBuff _damageBuff;
        private AlliedGroup _alliedGroup;
        private ExtraEffectHandler _extraEffectHandler;

        public override void Reset()
        {
            rb.linearVelocity = Vector2.zero;
            speed = 0;
            _damagePackage = null;
            _damageBuff = null;
        }

        public void SetData(ProjectileData projectileData, DamagePackage damagePackage,
            DamageBuff damageBuff, ExtraEffectHandler extraEffectHandler)
        {
            data = projectileData;
            speed = data.speed;
            spriteRenderer.sprite = data.sprite;
            transform.localScale = data.scale;
            _damagePackage = damagePackage;
            _damageBuff = damageBuff;
            _extraEffectHandler = extraEffectHandler;
        }

        public void ProjectileSetUp(Vector2 direction, AlliedGroup alliedGroup, int contacts)
        {
            if (direction == Vector2.zero) return;
            direction = direction.normalized;
            transform.right = direction;
            rb.linearVelocity = direction * speed;
            allowedContacts = contacts;
            _alliedGroup = alliedGroup;
        }

        public void HandleContact(GameObject contact)
        {
            ContactToHubAdapter hubAdapter = new(contact, _alliedGroup, _extraEffectHandler);
            if (!hubAdapter.IsValid || hubAdapter.Alie) return;
            hubAdapter.Apply(_damagePackage);
            hubAdapter.Apply(_damageBuff?.GetCopy());
            allowedContacts--;
            if (allowedContacts <= 0)
            {
                ReturnToPool();
            }
        }
    }
}