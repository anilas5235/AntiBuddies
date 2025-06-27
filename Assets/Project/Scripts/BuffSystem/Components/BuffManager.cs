using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Project.Scripts.BuffSystem.Buffs;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    /// <summary>
    /// Manages all buffs for an entity, handling addition, removal, and lookup.
    /// </summary>
    public class BuffManager : MonoBehaviour
    {
        /// <summary>
        /// Dictionary mapping buff names to lists of buffs.
        /// </summary>
        private readonly Dictionary<string, List<IBuff>> _buffs = new();

        /// <summary>
        /// Adds a buff to the manager and registers it with the central ticker.
        /// </summary>
        /// <param name="buff">The buff to add.</param>
        public void AddBuff(IBuff buff)
        {
            if (buff == null)
            {
                Debug.LogError("Buff is null");
                return;
            }

            if (!buff.ShouldBuffBeAdded(this)) return;
            CentralBuffTicker.Instance.RegisterBuff(buff);
            AddBuffToDictionary(buff);
        }

        /// <summary>
        /// Adds a buff to the internal dictionary.
        /// </summary>
        /// <param name="buff">The buff to add.</param>
        private void AddBuffToDictionary([NotNull] IBuff buff)
        {
            string key = buff.Name;
            if (_buffs.TryGetValue(key, out List<IBuff> buffList))
            {
                buffList.Add(buff);
            }
            else
            {
                _buffs.Add(key, new List<IBuff> { buff });
            }

            buff.OnBuffAdded();
        }

        /// <summary>
        /// Removes a buff from the manager and unregisters it from the buff group.
        /// </summary>
        /// <param name="buff">The buff to remove.</param>
        public void RemoveBuff(IBuff buff)
        {
            if (buff == null)
            {
                Debug.LogError("Buff is null");
                return;
            }

            buff.BuffGroup.UnregisterBuff(buff);
            RemoveBuffFromDictionary(buff);
        }

        /// <summary>
        /// Removes a buff from the internal dictionary.
        /// </summary>
        /// <param name="buff">The buff to remove.</param>
        private void RemoveBuffFromDictionary(IBuff buff)
        {
            if (!_buffs.TryGetValue(buff.Name, out List<IBuff> buffList)) return;
            buffList.Remove(buff);
        }

        /// <summary>
        /// Tries to get the first buff of a given type name.
        /// </summary>
        /// <param name="typeName">The buff type name.</param>
        /// <param name="buff">The first buff found, or null.</param>
        /// <returns>True if a buff was found; otherwise, false.</returns>
        internal bool TryGetFirstBuff(string typeName, out IBuff buff)
        {
            buff = null;
            if (!_buffs.TryGetValue(typeName, out List<IBuff> buffList)) return false;
            if (buffList.Count <= 0) return false;
            buff = buffList.First();
            return true;
        }

        /// <summary>
        /// Gets the count of buffs of a given type.
        /// </summary>
        /// <param name="typeName">The buff type name.</param>
        /// <returns>The number of buffs of that type.</returns>
        public int GetBuffCount(string typeName)
        {
            return _buffs.TryGetValue(typeName, out List<IBuff> buffList) ? buffList.Count : 0;
        }

        /// <summary>
        /// Checks if the manager has any buffs of a given type.
        /// </summary>
        /// <param name="typeName">The buff type name.</param>
        /// <returns>True if at least one buff of that type exists.</returns>
        public bool HasBuff(string typeName) => GetBuffCount(typeName) > 0;
    }
}