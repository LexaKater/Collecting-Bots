namespace Zenject
{
    public class LateTickablesTaskUpdater : TaskUpdater<ILateTickable>
    {
        protected override void UpdateItem(ILateTickable task)
        {
#if ZEN_INTERNAL_PROFILING
            using (ProfileTimers.CreateTimedBlock("User Code"))
#endif
#if UNITY_EDITOR
            using (ProfileBlock.Start("{0}.LateTick()", task.GetType()))
#endif
            {
                task.LateTick();
            }
        }
    }
}