using UnityEngine;

namespace TacticalPanicCode
{
    public class IdleSomeTimeUnitBehavior : UnitBehavior
    {
        private float time;

        public IdleSomeTimeUnitBehavior(float time)
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
