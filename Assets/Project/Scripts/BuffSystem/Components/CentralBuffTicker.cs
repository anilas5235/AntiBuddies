using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.BuffSystem.Buffs;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    /// <summary>
    /// CentralBuffTicker is a singleton class that manages the central buff ticker in the game.
    /// </summary>
    public class CentralBuffTicker : MonoBehaviour
    {
        private static CentralBuffTicker _instance;
        [SerializeField] private List<BuffGroup> buffGroups;
        [SerializeField] private int initialBuffGroups = 3;
        [SerializeField] private int maxBuffGroups = 30;

        private Coroutine _coroutine;
        private BuffGroup _currBuffGroup;


        public static CentralBuffTicker Instance
        {
            get
            {
                if (_instance) return _instance;
                _instance = FindFirstObjectByType<CentralBuffTicker>(); // Try to find an existing instance in the scene
                if (_instance) return _instance;
                GameObject obj = new("CentralBuffTicker"); // If no instance is found, create a new one
                _instance = obj.AddComponent<CentralBuffTicker>();
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                InitBuffGroups();
            }
        }

        private void OnEnable()
        {
            _coroutine = StartCoroutine(Ticking());
        }

        private void OnDisable()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private void InitBuffGroups()
        {
            for (int i = 0; i < initialBuffGroups; i++)
            {
                CreateBuffGroup();
            }
        }

        public BuffGroup RegisterBuff(IBuff buff)
        {
            if (buff == null)
            {
                Debug.LogError("Buff is null");
                return null;
            }

            if (_currBuffGroup.RegisterBuff(buff))
            {
                return _currBuffGroup;
            }

            UpdateCurrentBuffGroup();
            
            if (_currBuffGroup.RegisterBuff(buff))
            {
                return _currBuffGroup;
            }
            
            Debug.LogError("No available buff groups");
            return null;
        }

        private BuffGroup GetGroupForNextBuffs()
        {
            BuffGroup group = buffGroups.Where(group => group.HasSpace)
                .OrderBy(group => group.BuffCount)
                .FirstOrDefault();
            return group ?? CreateBuffGroup();
        }

        private void UpdateCurrentBuffGroup()
        {
            _currBuffGroup = GetGroupForNextBuffs();
        }

        private BuffGroup CreateBuffGroup()
        {
            if (buffGroups.Count >= maxBuffGroups)
            {
                throw new Exception("Max buff groups reached");
            }

            BuffGroup buffGroup = new();
            buffGroups.Add(buffGroup);
            return buffGroup;
        }


        private IEnumerator Ticking()
        {
            WaitForFixedUpdate wait = new();
            int index = 0;
            while (true)
            {
                UpdateCurrentBuffGroup();
                
                buffGroups[index].Tick();
                index = (index + 1) % buffGroups.Count;
                
                yield return wait;
            }
        }
    }
}