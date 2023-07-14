using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectHP : MonoBehaviour
{
    [SerializeField] private GameObject obj_Hp;
    [SerializeField] private TMP_Text tmp_hp;
    [SerializeField] private GameObject obj_objBox;
    [SerializeField] private GameObject obj_player;

    void Update()
    {
        
        if(WaveController.Instance.m_waveObjectCount > 0 && Vector2.Distance(obj_objBox.transform.GetChild(0).transform.position, obj_player.transform.position) < 5)
        {
            obj_Hp.SetActive(true);
            tmp_hp.text = obj_objBox.transform.GetChild(0).GetComponent<ObjectInfo>().m_currentHP + "/" + WaveController.Instance.obj_objectBox.transform.GetChild(0).GetComponent<ObjectInfo>().m_hp;
        }
        else
        {
            obj_Hp.SetActive(false);
        }

    }
}
