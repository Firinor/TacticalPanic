namespace TacticalPanicCode.UnitBehaviours
{
    public abstract class UnitBehaviour
    {
        public delegate void Delegate();
        private Delegate exitDelegate;
        public event Delegate exitEvent
        {
            add { exitDelegate = value; }
            remove { }
        }

        public virtual void Enter() { }
        public virtual void Exit()
        {
            HardExit();
        }
        public virtual void HardExit()
        {
            exitDelegate?.Invoke();
        }
        public virtual void Check() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}
