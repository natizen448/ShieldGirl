using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Jump,
    Attack,
    Block,
    JumpAttack,
    JumpBlock,
    JumpUlt,
    AttackUlt

}

public enum Attack
{
    Attack1,
    Attack2
}

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerState playerState = PlayerState.Idle;
    public Attack attack = Attack.Attack2;
    public int m_attackdamage;
    [HideInInspector] public int m_currentAttackDamage;

    private void Start()
    {
        m_currentAttackDamage = m_attackdamage;
    }

}
