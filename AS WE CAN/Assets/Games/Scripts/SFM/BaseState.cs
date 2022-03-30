
public enum PlayerState
{
    Movement, Hurt, Death, Attack, Action, Speech
}

public abstract class BaseState
{

    protected PlayerSFM ctrl;
    protected StateParameter para;

    public virtual void OnEnter()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
