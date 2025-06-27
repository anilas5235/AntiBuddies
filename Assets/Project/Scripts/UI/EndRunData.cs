using UnityEngine;

namespace Project.Scripts.UI
{
    [CreateAssetMenu(fileName = "EndRunData", menuName = "UI/EndRunData")]
    public class EndRunData : ScriptableObject
    {
        public string Message;

        public void SetMessage(bool win)
        {
            Message = win ? "Congratulations! You have won the run!" 
                : "You have lost the run. Better luck next time!";
        }
    }
}