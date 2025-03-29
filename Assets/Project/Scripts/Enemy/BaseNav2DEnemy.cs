using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Enemy
{
    public class BaseNav2DEnemy : MonoBehaviour
    {
        protected NavMeshAgent agent;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }
}
