namespace Zenject
{
    public class TickablesTaskUpdater : TaskUpdater<ITickable>
    {
        protected override void UpdateItem(ITickable task)
        {
#if ZEN_INTERNAL_PROFILING
            using (ProfileTimers.CreateTimedBlock("User Code"))
#endif
#if UNITY_EDITOR
            using (ProfileBlock.Start("{0}.Tick()", task.GetType()))
#endif
            {
                task.Tick();
            }
        }
    }
}