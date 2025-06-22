using UnityEngine;

namespace Project.Scripts.ResourceSystem.Gold
{
    /// <summary>
    /// Drops a specified amount of gold at the object's position.
    /// </summary>
    public class GoldDropper : MonoBehaviour
    {
        /// <summary>
        /// Amount of gold to drop.
        /// </summary>
        [SerializeField] private int goldAmount = 3;

        /// <summary>
        /// Spawns gold at the current position.
        /// </summary>
        public void Drop()
        {
            GoldSpawner.Instance.SpawnGold(goldAmount, transform.position);
        }
    }
}