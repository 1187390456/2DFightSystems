public class P_StateMachine
{
    public P_State currentState;
    public P_State lastState;

    public void Init(P_State startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(P_State newState)
    {
        currentState.Exit();

        lastState = currentState;
        currentState = newState;

        currentState.Enter();
    }
}