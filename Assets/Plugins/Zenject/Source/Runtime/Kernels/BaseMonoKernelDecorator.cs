namespace Zenject
{
    public abstract class BaseMonoKernelDecorator : IDecoratableMonoKernel
    {
        [Inject] 
        protected IDecoratableMonoKernel DecoratedMonoKernel;

        public virtual bool ShouldInitializeOnStart() => DecoratedMonoKernel.ShouldInitializeOnStart();
        public virtual void Initialize() => DecoratedMonoKernel.Initialize();
        public virtual void Update() => DecoratedMonoKernel.Update();
        public virtual void FixedUpdate() => DecoratedMonoKernel.FixedUpdate();
        public virtual void LateUpdate() => DecoratedMonoKernel.LateUpdate();
        public virtual void Dispose() => DecoratedMonoKernel.Dispose();
        public virtual void LateDispose() => DecoratedMonoKernel.LateDispose();
    }
}