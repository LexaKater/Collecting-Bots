namespace Zenject.SpaceFighter
{
    public interface IEnemyState
    {
        void EnterState();
        void ExitState();
        void Update();
        void FixedUpdate();
    }
}