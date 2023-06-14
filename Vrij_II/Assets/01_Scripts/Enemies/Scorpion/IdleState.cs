
using UnityEngine;

namespace Scorpion {
    public class IdleState : BaseState<ScorpionController> {

        public override void OnStart() {}

        public override void OnUpdate() {
            if(Vector3.Distance(runner.target.position, transform.position) <= runner.viewDistance) {
                runner.SwitchState(ScorpionState.Attacking);
            }
        }

        public override void OnEnd() {}

    }

}