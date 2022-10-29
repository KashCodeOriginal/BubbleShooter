using UnityEngine.Events;

public interface IDestroyable
{
    public event UnityAction OnBallDestroyed;
}