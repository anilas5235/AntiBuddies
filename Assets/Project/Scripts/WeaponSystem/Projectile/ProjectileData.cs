using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "WeaponSystem/ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        public GameObject prefab;
        public Sprite sprite;
        public Vector3 scale = Vector3.one;
        public float speed;
        public int contacts = 1;
    }
}