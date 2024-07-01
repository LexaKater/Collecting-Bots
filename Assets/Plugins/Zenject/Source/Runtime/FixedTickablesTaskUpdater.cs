namespace Zenject
{
    public class FixedTickablesTaskUpdater : TaskUpdater<IFixedTickable>
    {
        protected override void UpdateItem(IFixedTickable task)
        {
#if ZEN_INTERNAL_PROFILING
            using (ProfileTimers.CreateTimedBlock("User Code"))
#endif
#if UNITY_EDITOR
            using (ProfileBlock.Start("{0}.FixedTick()", task.GetType()))
#endif
            {
                task.FixedTick();
            }
        }
    }
}