namespace Zenject
{
    public class DecoratableMonoKernel : IDecoratableMonoKernel
    {
        [InjectLocal] 
        public TickableManager TickableManager { get; protected set; } = null;

        [InjectLocal]
        public InitializableManager InitializableManager { get; protected set; } = null;

        [InjectLocal]
        public DisposableManager DisposablesManager { get; protected set; } = null;
        
        
        public virtual bool ShouldInitializeOnStart() => true;
        
        public virtual void Initialize()
        {
            InitializableManager.Initialize();
        }

        public void Update()
        {
            TickableManager.Update();
        }

        public void FixedUpdate()
        {
            TickableManager.FixedUpdate();
        }

        public void LateUpdate()
        {
            TickableManager.LateUpdate();
        }

        public void Dispose()
        {
            DisposablesManager.Dispose();
        }

        public void LateDispose()
        {
            DisposablesManager.LateDispose();
        }
    }
}