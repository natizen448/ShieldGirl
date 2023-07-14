using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomDrop : MonoBehaviour
{
    [HideInInspector] public ObjectRandDropPool pool;
    
    [SerializeField] private float m_coolTime = 1.5f;
    private float m_currentCoolTime;

    public void GenerateRandomObject()
    {
        m_currentCoolTime = m_coolTime + 1f;

        Vector2 target = WaveController.Instance.obj_objectBox.transform.GetChild(0).transform.position;

        float randomX = Random.Range(target.x + pool.minX, target.x + pool.maxX);
        float randomY = Random.Range(target.y + pool.minY, target.y + pool.maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        
        transform.position = spawnPosition;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 361));

        Invoke("ReturnObject", m_coolTime);
    }

    private void Update()
    {
        m_currentCoolTime -= Time.deltaTime;
        if(m_currentCoolTime < 0)
        {
            m_currentCoolTime = m_coolTime + 1f;
            ReturnObject();
        }
    }

    private void ReturnObject()
    {
        pool.ReturnObject(this);
    }


}
