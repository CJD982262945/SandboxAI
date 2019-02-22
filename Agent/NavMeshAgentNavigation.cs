﻿using UnityEngine;
using UnityEngine.AI;

namespace SandboxAI {
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshAgentNavigation : MonoBehaviour, IAgentNavigation {
        public Vector3 velocity {
            get { return _agent.velocity; }
        }

        public bool hasArrived {
            get { return !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance; }
        }

        public bool failed {
            get { return !_agent.isOnNavMesh; }
        }

        NavMeshAgent _agent;
        Transform _target;

        public void MoveTo(Transform target, float distance = 0) {
            _agent.enabled = true;

            if (!_agent.isOnNavMesh)
                return;

            _agent.stoppingDistance = distance;
            _agent.destination = target.transform.position;
            _agent.isStopped = false;

            _target = target;
        }

        public void StopMoving() {
            if (_agent.isOnNavMesh) {
                _agent.isStopped = true;
                _agent.ResetPath();
            }
        }

        public void Disable() {
            _agent.enabled = false;
        }

        void Start() {
            _agent = GetComponent<NavMeshAgent>();
        }

        void Update() {
            if (!_agent.enabled || _target == null)
                return;

            var diff = _agent.destination - _target.position;
            if (diff.sqrMagnitude > 1) {
                _agent.destination = _target.position;
            }
        }
    }
}