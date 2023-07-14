using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBox : MonoBehaviour
{
    [SerializeField] private float m_dropSpeed;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    void Update()
    {
        if (WaveController.Instance.m_isCreateEnd && !ObjectManager.Instance.m_isBumpGround && !ObjectManager.Instance.m_isBlocked)
        {
            rb.simulated = false;

            transform.Translate(Vector2.down * m_dropSpeed * Time.deltaTime);
        }
        else
        {
            
            rb.simulated = true;
        }

    }
}
