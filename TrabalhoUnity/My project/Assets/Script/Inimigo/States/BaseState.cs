public abstract class BaseState
{
    public Inimigo enemy;
    public StateMachine stateMachine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
