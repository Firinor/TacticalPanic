using UnityEngine;

namespace TacticalPanicCode.UnitBehaviours
{
    public class IdleSomeTimeUnitBehaviour : UnitBehaviour
    {
        private float time;

        public IdleSomeTimeUnitBehaviour(float time)
        {
            this.time = time;
        }

        public override void Update()
        {
            time -= Time.deltaTime;
            if(time < 0)
                Exit();
        }
    }
}
