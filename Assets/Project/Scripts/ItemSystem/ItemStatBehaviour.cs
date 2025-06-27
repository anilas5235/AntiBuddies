using System;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.StatSystem.Stats;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.ItemSystem
{
    [CreateAssetMenu(fileName = "ItemStatBehaviour", menuName = "ItemSystem/StatBehaviour")]
    public class ItemStatBehaviour : ItemBehaviour
    {
        [FormerlySerializedAs("statModification")] [SerializeField]
        StatPackage[] statPackage;

        public override void OnAdded()
        {
            foreach (var modification in statPackage)
            {
                GlobalVariables.Instance.PlayerStatGroup.ModifyStat(modification);
            }
        }

        public override void OnRemoved()
        {
            foreach (var modification in statPackage)
            {
                GlobalVariables.Instance.PlayerStatGroup.ModifyStat(modification.Inverse());
            }
        }

        public override string ToString()
        {
            string str = "";
            foreach (var package in statPackage)
            {
                if (package.Amount > 0)
                {
                    str += "<color=green>";
                    str += "+";
                }
                else
                {
                    str += "<color=red>";
                }

                str += package.Amount;
                if (package.StatType.IsPercentage)
                    str += "%";
                str += "</color> "  + package.StatType.Name + "\n";
            }
            return str;
        }
    }
}