using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_jumpPower;
    [SerializeField] private float m_dropPower;
    [SerializeField] private float m_ultPower;
    [SerializeField] private float m_ultMoveSpeed;

    [SerializeField] private Vector2 boxCastSize;
    [SerializeField] private float m_boxCastMaxDistance;

    [SerializeField] private GameObject obj_headColider;
    [SerializeField] private GameObject obj_UltPlayerState;
    [SerializeField] private GameObject obj_DefaultPlayerState;

    public bool m_isBumped;
    private bool m_isLanded = true;
    private bool m_isUlt;

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerAttack playerAttack;
    public Ultimate ultimate;

    private Vector3 ultStartPos;

    [SerializeField] private ParticleSystem landing;
    [SerializeField] private ParticleSystem jump;

    [SerializeField] private AudioSource ac;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;
    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckLanding();
        CheckUltJump();
        KeyboardUseUlt();
        anim.SetBool("isJump", rb.velocity.y != 0 ? true : false);

        obj_headColider.SetActive(!IsOnGround());

        if(m_isUlt)
        {
            PerformUltimateMove();
        }
    }

    private void KeyboardUseUlt()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ultimate.m_isCanUseUlt)
            {
                m_isUlt = true;

                ultStartPos = transform.position;

                playerAttack.JumpUlt();

                ChangeStateToUltMode();

                ultimate.ResetGauge();
            }
        }
    }


    private void CheckLanding()
    {
        if (rb.velocity.y < 0 && IsOnGround() && !m_isLanded)
        {
            landing.Play();
            m_isLanded = true;
            rb.velocity = Vector2.zero;
            anim.Play("Idle");
            SoundPlay(landSound);
            PlayerManager.Instance.playerState = PlayerState.Idle;
            PlayerManager.Instance.attack = Attack.Attack2;
        }
    }

    public void FastDrop()
    {
        rb.AddForce(new Vector2(0, -m_dropPower), ForceMode2D.Impulse);
    }

    public void Btn_Jump()
    {   
        if (GameManager.Instance.gameState == GameState.End)
                return;

        if (IsOnGround())
        {
            rb.AddForce(new Vector2(0, m_jumpPower), ForceMode2D.Impulse);

            anim.Play("Jump");

            jump.Play();

            SoundPlay(jumpSound);

            PlayerManager.Instance.playerState = PlayerState.Jump;

            m_isBumped = false;
            m_isLanded = false;

            ultimate.AddUltGauge(0.2f);
        }

    }

    private void PerformUltimateMove()
    {   
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, ultStartPos.y + m_ultPower), m_ultMoveSpeed * Time.deltaTime);
       
    }

    private void CheckUltJump()
    {
        if(ultimate.m_isCanUseUlt && ultimate.TouchCheck())
        {
            m_isUlt = true;

            ultStartPos = transform.position;

            playerAttack.JumpUlt();

            ChangeStateToUltMode();

            ultimate.ResetGauge();
        }
    }
    
    private void ChangeStateToUltMode()
    {
        obj_UltPlayerState.SetActive(true);
        obj_DefaultPlayerState.SetActive(false);

        PlayerManager.Instance.playerState = PlayerState.JumpUlt;

        StartCoroutine(RollBackDefaultCharater());
    }

    public bool IsOnGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, m_boxCastMaxDistance, LayerMask.GetMask("Ground"));
        return (raycastHit.collider != null);
    }
    void OnDrawGizmos()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, m_boxCastMaxDistance, LayerMask.GetMask("Ground"));

        Gizmos.color = Color.red;
        if (raycastHit.collider != null)
        {
            Gizmos.DrawRay(transform.position, Vector2.down * raycastHit.distance);
            Gizmos.DrawWireCube(transform.position + Vector3.down * raycastHit.distance, boxCastSize);
        }
        else
        {
            Gizmos.DrawRay(transform.position, Vector2.down * m_boxCastMaxDistance);
        }
    }

    private IEnumerator RollBackDefaultCharater()
    {
        yield return YieldCache.WaitForSeconds(0.3f);

        rb.velocity = Vector3.zero;

        obj_UltPlayerState.SetActive(false);
        obj_DefaultPlayerState.SetActive(true);

        m_isUlt = false;

        PlayerManager.Instance.playerState = PlayerState.Idle;
    }

    private void SoundPlay(AudioClip clip)
    {
        ac.Stop();
        ac.clip = clip;
        ac.Play();
    }

    public void InIt()
    {
        ultimate.ResetGauge();
        m_isBumped = false;
        m_isLanded = true;
        m_isUlt = false;
    }

}
