namespace Game.Interfaces 
{
    public interface IStateMachine
    {
        void ChangeState(ICharacterState newState);
        ICharacterState CurrentState { get; }
    }
}