namespace CityBuilder.Worker.StateMachine.States
{
    public class TakingStone : IState
    {
        private readonly Worker _worker;
        private bool _isStoneVisible;

        public TakingStone(Worker worker)
        {
            _worker = worker;
        }

        public void OnEnter()
        {
            _isStoneVisible = true;
            _worker.TakeStone(_isStoneVisible);
        }

        public void Tick() { }

        public void OnExit() { }
    }
}