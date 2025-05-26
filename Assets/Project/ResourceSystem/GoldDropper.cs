using UnityEngine;

namespace Project.ResourceSystem
{
    public class GoldDropper : MonoBehaviour
    {
        [SerializeField] private int goldAmount = 3;

        public void Drop()
        {
            GoldSpawner.Instance.SpawnGold(goldAmount, transform.position);
        }
    }
}