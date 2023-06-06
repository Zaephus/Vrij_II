
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wurm {

    public class IdleState : BaseState<WurmController> {

        public override void OnStart() {
            runner.agent.SetDestination(transform.position);
        }

        public override void OnUpdate() {}

        public override void OnEnd() {}

    }

}