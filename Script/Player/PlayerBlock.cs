using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerBlock : MonoBehaviour
{
    [SerializeField] private float m_blockCoolTime;
    [SerializeField] private Vector2 boxCastSize;
    [SerializeField] private Vector3 boxCastTransform;
    [SerializeField] private float m_boxCastMaxDistance;

    [SerializeField] private Image m_sheildImage;
    [SerializeField] private GameObject obj_fallObject;
    [SerializeField] private ParticleSystem blockEffect;
    [SerializeField] private ParticleSystem blockUltEffect;
    [SerializeField] private AudioSource ac;
    [SerializeField] private AudioClip blockObjectSound;
    [SerializeField] private AudioClip blockUltSound;

    [SerializeField] private float m_pushPower;

    private bool m_isCanBlock = true;

    public Ultimate ultimate;

    public bool IsOnObject()
    {
        Vector2 castOrigin = transform.position + boxCastTransform;
        RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(castOrigin, boxCastSize, 0f, Vector2.up, m_boxCastMaxDistance, LayerMask.GetMask("Object"));

        foreach (RaycastHit2D hit in raycastHits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
    }

    #region ±âÁî¸ð
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 castOrigin = transform.position + boxCastTransform;
        Gizmos.DrawWireCube(castOrigin, boxCastSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(castOrigin, Vector2.up * m_boxCastMaxDistance);

    }
    #endregion

    private void Update()
    {
        if (!m_isCanBlock)
        {
            m_sheildImage.gameObject.SetActive(true);
            m_sheildImage.fillAmount -=  Time.deltaTime / m_blockCoolTime;
        }
        else
        {
            m_sheildImage.gameObject.SetActive(false);
            m_sheildImage.fillAmount = 1;

        }
    }

    public void Btn_Block()
    {
        if (IsOnObject() && m_isCanBlock &&  PlayerManager.Instance.playerState != PlayerState.JumpUlt)
        {
            m_isCanBlock = false;

            PushBack();

            StartCoroutine(CoolDown());

            SoundPlay(blockObjectSound);

            blockEffect.Play();

            ultimate.AddUltGauge(0.1f);
        }
    }

    private void PushBack()
    {
        ObjectManager.Instance.m_isBlocked = true;

        obj_fallObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        obj_fallObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, m_pushPower), ForceMode2D.Impulse);

        StartCoroutine(BlockEnded());
    }

    public void SheildUltimate()
    {
        if(ultimate.m_isCanUseUlt && ScoreBoard.Instance.m_hp < 4)
        {
            ScoreBoard.Instance.AddHP();

            SoundPlay(blockUltSound);

            blockUltEffect.Play();

            ultimate.ResetGauge();
        }
    }


    private IEnumerator BlockEnded()
    {
        yield return YieldCache.WaitForSeconds(1f);
        ObjectManager.Instance.m_isBlocked = false;
    }

    private IEnumerator CoolDown()
    {   
        yield return YieldCache.WaitForSeconds(m_blockCoolTime);
        m_isCanBlock = true;

    }

    private void SoundPlay(AudioClip clip)
    {
        ac.Stop();
        ac.clip = clip;
        ac.Play();
    }

    public void Init()
    {

        ultimate.ResetGauge();
        m_sheildImage.gameObject.SetActive(false);
        m_sheildImage.fillAmount = 1;
        m_isCanBlock = true;
    }
}
