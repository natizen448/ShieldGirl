using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    public bool m_isBumpGround = false;
    public bool m_isCanDrop = false;
    public bool m_isBlocked = false;

    public void Init()
    {
        m_isBumpGround = false;
        m_isCanDrop = false;
        m_isBlocked = false;
    }
}
