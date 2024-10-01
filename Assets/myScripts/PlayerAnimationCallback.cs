using System;
using UnityEngine;

public class PlayerAnimationCallback : MonoBehaviour
{
    private Action onPlayerDeath;
    private Action onPlayerStartAttack;
    private Action onPlayerStopAttack;
    private Action onPlayerRespawn;

    public void SetAttackStartCallback(Action callback)
    {
        onPlayerStartAttack = callback;
    }

    public void SetAttackStopCallback(Action callback)
    {
        onPlayerStopAttack = callback;
    }

    public void PlayerAttackingStart()
    {
        onPlayerStartAttack?.Invoke();
    }

    public void PlayerAttackingFinish()
    {
        onPlayerStopAttack?.Invoke();
    }

}
