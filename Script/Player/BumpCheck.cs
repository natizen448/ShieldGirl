using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpCheck : MonoBehaviour
{  
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Object") && !playerMovement.m_isBumped)
        {
            playerMovement.FastDrop();
        }
    }


}
