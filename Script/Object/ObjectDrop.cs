
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrop : Singleton<ObjectDrop>
{
    public ObjectRandDropPool[] dropObject;

    public void RandomDrop(ObjectRandDropPool _dropObject,int minCreateCount, int maxCreateCount,float minForce, float maxForce)
    {
        for(int i = 0; i < Random.Range(minCreateCount, maxCreateCount); i++)
        {   
            float randomAngle = Random.Range(0f, 360f); // ·£´ýÇÑ °¢µµ¸¦ »ý¼º
            float randomForce = Random.Range(minForce, maxForce); // ·£´ýÇÑ ÈûÀ» »ý¼º

            Vector2 forceDirection = Quaternion.Euler(0f, 0f, randomAngle) * Vector2.right;

            var obj = _dropObject.GetObject();

            obj.GenerateRandomObject();
            obj.GetComponent<Rigidbody2D>().AddForce(forceDirection * randomForce, ForceMode2D.Impulse);
        }
    }

}
