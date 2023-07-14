using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : Singleton<DamageTextPool>
{
    [SerializeField] private GameObject obj_damageText;
    [HideInInspector] public Transform damageTextSpawnPos;

    Queue<DamageText> poolingObject = new Queue<DamageText>();

    private void Start()
    {
        Initialize(15);
    }

    public void Initialize(int initCount)
    {
        for (int j = 0; j < initCount; j++)
        {
            poolingObject.Enqueue(CreateNewObject());
        }
    }

    private DamageText CreateNewObject()
    {
        var newObj = Instantiate(obj_damageText).GetComponent<DamageText>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(null);
        return newObj;
    }


    public DamageText GetObject()
    {
        if (poolingObject.Count > 0)
        {
            var obj = poolingObject.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public void ReturnObject(DamageText obj)
    {
        obj.gameObject.SetActive(false);
        poolingObject.Enqueue(obj);
    }
}
