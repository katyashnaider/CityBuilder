namespace Workers.StateMachines
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}