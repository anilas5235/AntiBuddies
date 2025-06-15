using Project.Scripts.UI;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerEvents : MonoBehaviour
    {
        public void OnDeath()
        {
            UIManager.Instance.ShowEndRunMenu();
        }
    }
}