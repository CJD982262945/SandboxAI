﻿using UnityEngine;

namespace SandboxAI {
    public interface IAgentNavigation {
        Vector3 velocity {
            get;
        }

        bool hasArrived {
            get;
        }

        bool failed {
            get;
        }

        void MoveTo(Transform target, float distance = 0);
        void StopMoving();
        void Disable();
    }
}