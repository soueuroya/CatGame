using System;
using UnityEngine;

public class PlayerAnimationCallback : MonoBehaviour
{
    private Action onPlayerFinishDeath;
    private Action onPlayerStartAttack;
    private Action onPlayerStopAttack;

    public void SetAttackStartCallback(Action callback)
    {
        onPlayerStartAttack = callback;
    }

    public void SetAttackStopCallback(Action callback)
    {
        onPlayerStopAttack = callback;
    }

    public void SetDeathFinishCallback(Action callback)
    {
        onPlayerFinishDeath = callback;
    }

    public void PlayerAttackingStart()
    {
        onPlayerStartAttack?.Invoke();
    }

    public void PlayerAttackingFinish()
    {
        onPlayerStopAttack?.Invoke();
    }

    public void PlayerDeathFinish()
    {
        onPlayerFinishDeath?.Invoke();
    }

}
