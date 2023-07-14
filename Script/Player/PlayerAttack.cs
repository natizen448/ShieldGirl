using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int m_attackCount;
    [SerializeField] private float m_attackDelay;
    [SerializeField] private float ultDurationTime;
    private bool m_isUsingUlt = false;
    private bool m_isCanAttack = true;

    Animator anim;
    AttackArea attackArea;

    [SerializeField] private AudioSource acPlayer;
    [SerializeField] private AudioClip defaultAttack;
    [SerializeField] private AudioClip ultAttack;
    [SerializeField] private AudioClip useUltSound;
    

    [SerializeField] private ParticleSystem attack1;
    [SerializeField] private ParticleSystem attack2;
    [SerializeField] private ParticleSystem ultAttack1;
    [SerializeField] private ParticleSystem ultAttack2;
    [SerializeField] private ParticleSystem useUlt;

    public Ultimate ultimate;

    private void Start()
    {
        anim = GetComponent<Animator>();
        attackArea = GetComponent<AttackArea>();
    }

    private void Update()
    {
        if (attackArea.m_isAreaHoldAble )
        {
            if (attackArea.IsOnObject())
            {
                CreateAttackArea();
            }
        }

        AttackUltimate();
        KeyboardUseUlt();
    }

    private void KeyboardUseUlt()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ultimate.m_isCanUseUlt)
            {
                m_isUsingUlt = true;

                attackArea.ChangeArea(3.84f, 4.17f, 2.9f);

                ultimate.ResetGauge();

                StartCoroutine(UltDurationTimer());

                SoundPlay(useUltSound);

                useUlt.Play();
            }
        }
    }


    private void AttackUltimate()
    {
        if (ultimate.m_isCanUseUlt && ultimate.TouchCheck())
        {
            m_isUsingUlt = true;

            attackArea.ChangeArea(3.84f, 4.17f, 2.9f);

            ultimate.ResetGauge();

            StartCoroutine(UltDurationTimer());

            SoundPlay(useUltSound);

            useUlt.Play();
        }
    }

    IEnumerator UltDurationTimer()
    {   
        yield return YieldCache.WaitForSeconds(ultDurationTime);
        UltEnd();
    }

    private void UltEnd()
    {
        m_isUsingUlt = false;
        attackArea.ChangeArea(3.84f, 3.21f, 1.87f);
        m_attackCount = 1;
        useUlt.Stop();
    }

    public void Btn_Attack()
    {
        if (GameManager.Instance.gameState == GameState.End)
            return;

        if (PlayerManager.Instance.playerState != PlayerState.JumpUlt)
        {
            if (m_isUsingUlt)
            {
                m_attackCount = 2;

                AnimationSetting(ultAttack1, ultAttack2);

                SoundPlay(ultAttack);

                attackArea.AddDelay(attackArea.m_HoldDelay);
            }
            else
            {
                if (m_isCanAttack)
                {
                    m_isCanAttack = false;

                    m_attackCount = 1;

                    AnimationSetting(attack1, attack2);

                    SoundPlay(defaultAttack);

                    attackArea.AddDelay(attackArea.m_HoldDelay);

                    StartCoroutine(AttackDelay());
                }

            }

        }
    }

    IEnumerator AttackDelay()
    {
        yield return YieldCache.WaitForSeconds(m_attackDelay);
        m_isCanAttack = true;
    }

    public void JumpUlt()
    {
        m_attackCount = WaveController.Instance.obj_objectBox.transform.childCount;
        PlayerManager.Instance.m_currentAttackDamage = 9999;
        attackArea.AddDelay(0.7f);
    }

    /// <summary>
    /// 공격 버튼을 눌렀을시 공격 히트박스 생성
    /// </summary>
    private void CreateAttackArea()
    {
        if (m_attackCount > 0)
        {
            m_attackCount--;
            AttackSuccess();
        }
    }

    private void AttackSuccess()
    {
        attackArea.ObjectInfo.OnDamaged(PlayerManager.Instance.m_currentAttackDamage);

        if (!m_isUsingUlt)
            ultimate.AddUltGauge(0.025f);
    }


    /// <summary>
    /// 첫번째 공격 후 공격일시엔 두번째 애니메이션 재생
    /// </summary>
    private void AnimationSetting(ParticleSystem at1, ParticleSystem at2)
    {   

        if(PlayerManager.Instance.attack == Attack.Attack1)
        {
            AttackAnimSetting("Attack2", "JumpAttack2");
            at2.Play();

            PlayerManager.Instance.attack = Attack.Attack2;
        }
        else if(PlayerManager.Instance.attack == Attack.Attack2)
        {
            AttackAnimSetting("Attack1", "JumpAttack1");
            at1.Play();
            PlayerManager.Instance.attack = Attack.Attack1;
        }
    }

    /// <summary>
    /// 점프 공격인지 그냥 공격인지 판단
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="jumpAttack"></param>
    private void AttackAnimSetting(string attack, string jumpAttack)
    {
        if (IsOnGround())
        {
            anim.Play(attack);
        }
        else
        {
            anim.Play(jumpAttack);
        }
    }

    private bool IsOnGround()
    {
        if (PlayerManager.Instance.playerState == PlayerState.Jump)
            return false;
        else
            return true;
    }

    private void SoundPlay(AudioClip clip)
    {
        acPlayer.Stop();
        acPlayer.clip = clip;
        acPlayer.Play();
    }

    public void InIt()
    {
        ultimate.ResetGauge();
        UltEnd();
        m_isUsingUlt = false;
        m_isCanAttack = true;
    }

}
