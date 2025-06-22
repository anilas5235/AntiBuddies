using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    /// <summary>
    /// Singleton that manages ticking of all buff groups in the game.
    /// </summary>
    public class CentralBuffTicker : Singleton<CentralBuffTicker>
    {
        /// <summary>
        /// List of all buff groups managed by the ticker.
        /// </summary>
        [SerializeField] private List<BuffGroup> buffGroups;

        /// <summary>
        /// Number of buff groups to initialize at start.
        /// </summary>
        [SerializeField] private int initialBuffGroups = 3;

        /// <summary>
        /// Maximum number of buff groups allowed.
        /// </summary>
        [SerializeField] private int maxBuffGroups = 30;

        /// <summary>
        /// Reference to the ticking coroutine.
        /// </summary>
        private Coroutine _coroutine;

        /// <summary>
        /// The current buff group used for registration.
        /// </summary>
        private BuffGroup _currBuffGroup;

        private void OnEnable()
        {
            InitBuffGroups();
            _coroutine = StartCoroutine(Ticking());
        }

        private void OnDisable()
        {
            if (_coroutine == null) return;
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        /// <summary>
        /// Initializes the buff groups up to the initial count.
        /// </summary>
        private void InitBuffGroups()
        {
            while (buffGroups.Count < initialBuffGroups)
            {
                CreateBuffGroup();
            }
        }

        /// <summary>
        /// Registers a buff to the current buff group, or finds/creates a new group if needed.
        /// </summary>
        /// <param name="buff">The buff to register.</param>
        public void RegisterBuff(IBuff buff)
        {
            if (buff == null)
            {
                throw new ArgumentNullException(nameof(buff));
            }
            
            if (buff.BuffGroup != null)
            {
                throw new InvalidOperationException("Buff is already registered in a group.");
            }

            if (_currBuffGroup.IsFull)
            {
                UpdateCurrentBuffGroup();
            }

            if (_currBuffGroup.RegisterBuff(buff)) return;
            Debug.LogError("No available buff groups");
        }

        /// <summary>
        /// Gets the next available buff group for new buffs, or creates one if needed.
        /// </summary>
        /// <returns>The buff group with available space.</returns>
        private BuffGroup GetGroupForNextBuffs()
        {
            BuffGroup group = buffGroups.Where(group => group.HasSpace)
                .OrderBy(group => group.BuffCount)
                .FirstOrDefault();
            return group ?? CreateBuffGroup();
        }

        /// <summary>
        /// Updates the current buff group to the next available one.
        /// </summary>
        private void UpdateCurrentBuffGroup()
        {
            _currBuffGroup = GetGroupForNextBuffs();
        }

        /// <summary>
        /// Creates a new buff group and adds it to the list.
        /// Throws an exception if the maximum number of groups is reached.
        /// </summary>
        /// <returns>The newly created buff group.</returns>
        private BuffGroup CreateBuffGroup()
        {
            if (buffGroups.Count >= maxBuffGroups)
            {
                throw new InvalidOperationException("Max buff groups reached (" + maxBuffGroups + ")");
            }

            BuffGroup buffGroup = new();
            buffGroups.Add(buffGroup);
            return buffGroup;
        }

        /// <summary>
        /// Coroutine that ticks each buff group in a round-robin fashion.
        /// </summary>
        private IEnumerator Ticking()
        {
            WaitForFixedUpdate wait = new();
            int index = 0;
            while (true)
            {
                UpdateCurrentBuffGroup();

                // Tick the current buff group and move to the next.
                buffGroups[index].Tick();
                index = ++index % buffGroups.Count;

                yield return wait;
            }
        }
    }
}