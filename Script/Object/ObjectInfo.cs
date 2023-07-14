using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Pool;

public class ObjectInfo : MonoBehaviour
{   
    [SerializeField] private float m_dropSpeed;

    public int m_score;

    public int m_currentHP;
    public int m_hp;

    [SerializeField] private int m_dropObjectIndex;

    [SerializeField] private int m_minGold;
    [SerializeField] private int m_maxGold;

    public float m_objectLength;

    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public bool m_isCanDrop = false;

    [SerializeField] private AudioClip dead;
    [SerializeField] private AudioSource ac;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.bodyType = ObjectManager.Instance.m_isBlocked ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;

        if(GameManager.Instance.gameState == GameState.End)
        {
            DestroyObject();
        }
    }

    public void Initialization()
    {
        m_currentHP = m_hp;
    }

    public void OnDamaged(int damage)
    {
        m_currentHP -= damage;

        var damageText = DamageTextPool.Instance.GetObject();
        damageText.GetComponent<DamageText>().ShowDamage(PlayerManager.Instance.m_currentAttackDamage);

        if (m_currentHP <= 0)
        {
            WaveController.Instance.SoundPlayObject(dead);

            ObjectDestroyed();
        }
    }

    private void ObjectDestroyed()
    {
        DestroyObject();

        ObjectManager.Instance.m_isBumpGround = false;

        ScoreBoard.Instance.AddScore(m_score);

        DropRandomGold();

        ObjectDrop.Instance.RandomDrop(ObjectDrop.Instance.dropObject[m_dropObjectIndex],10,12,0,3);
        ObjectDrop.Instance.RandomDrop(ObjectDrop.Instance.dropObject[0], 10, 20, 0, 3);
    }

    private void DestroyObject()
    {
        WaveController.Instance.ReturnObject(this);


    }

    private void DropRandomGold()
    {
        int gold = Random.Range(m_minGold, m_maxGold);

        ScoreBoard.Instance.AddGold(gold);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            OnHitPlayer();
        }
    }

    private void OnHitPlayer()
    {
        ObjectManager.Instance.m_isBumpGround = true;

        ScoreBoard.Instance.OnDamaged(1);

    }

    

}
