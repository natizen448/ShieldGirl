using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour
{
    [SerializeField] private GameObject obj_player;
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_cameraZ = -10;
    [SerializeField] private float m_cameraY;

    void FixedUpdate()
    {
        Vector3 TargetPos = new Vector3(obj_player.transform.position.x, obj_player.transform.position.y + m_cameraY, m_cameraZ);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * m_moveSpeed);
    }
}