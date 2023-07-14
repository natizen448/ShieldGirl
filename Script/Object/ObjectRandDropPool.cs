using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandDropPool : MonoBehaviour
{
    [SerializeField] private GameObject[] obj_randomDropObject;
    [SerializeField] private int maxCreateCount;

    public float minX; // ���� ������ �ּ� X ��ǥ
    public float maxX; // ���� ������ �ִ� X ��ǥ
    public float minY; // ���� ������ �ּ� Y ��ǥ
    public float maxY; // ���� ������ �ִ� Y ��ǥ

    Queue<ObjectRandomDrop> poolingObject = new Queue<ObjectRandomDrop>();

    private void Start()
    {
        Initialize(maxCreateCount);
    }

    public void Initialize(int initCount)
    {
        for (int j = 0; j < initCount; j++)
        {
            poolingObject.Enqueue(CreateNewObject());
        }
    }

    private ObjectRandomDrop CreateNewObject()
    {
        var newObj = Instantiate(obj_randomDropObject[Random.Range(0, obj_randomDropObject.Length)]).GetComponent<ObjectRandomDrop>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        newObj.GetComponent<ObjectRandomDrop>().pool = this;
        return newObj;
    }


    public ObjectRandomDrop GetObject()
    {   

        if (poolingObject.Count > 0)
        {
            var obj = poolingObject.Dequeue();
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }
        else
        {
            var newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public void ReturnObject(ObjectRandomDrop obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        poolingObject.Enqueue(obj);
    }
}
