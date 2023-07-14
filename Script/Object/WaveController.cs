using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class WaveController : Singleton<WaveController>
{   
    [SerializeField] private GameObject[] obj_fallingObject;
    public GameObject obj_objectBox;
    
    [SerializeField] private int m_MinObjectCount;
    [SerializeField] private int m_MaxObjectCount;
    [SerializeField] private float m_createDelay;
    [SerializeField] private float m_createTurnDelay;

    public float m_WaveTime;
    public float m_currentWaveTime;

    [SerializeField] private AudioSource acObject;

    Queue<ObjectInfo> poolingObject = new Queue<ObjectInfo>();

    [SerializeField] private float m_spawnPos;

    Vector2 defaultPos;

    [HideInInspector] public bool m_isCreateEnd = false;

    [HideInInspector] public int m_waveObjectCount = 0;

    IEnumerator makeObject;
    IEnumerator makingDelay;
    
    private void Start()
    {
        defaultPos = transform.position;
        Initialize(20);
        CreateObject();  
    }

    public void Init()
    {

        for(int i = 0; i < transform.childCount-1; i++)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        m_waveObjectCount = 0;
        m_currentWaveTime = m_WaveTime;
        defaultPos = transform.position;
        Initialize(20);
        CreateObject();
    }

    private void Update()
    {
        m_currentWaveTime -= Time.deltaTime;

        if(m_currentWaveTime < 0)
        {
            GameManager.Instance.gameState = GameState.End;
            GameOver.Instance.GameOverSet();
        }
    }

    public void Initialize(int initCount)
    {
        for (int j = 0; j < initCount; j++)
        {
            poolingObject.Enqueue(CreateNewObject(ControllSpawnRandomPercent()));
        }
    }

    public int ControllSpawnRandomPercent()
    {
        int random = Random.Range(0, 101);
        return random > 75 ? 1 : 0;
    }


    private ObjectInfo CreateNewObject(int number)
    {
        var newObj = Instantiate(obj_fallingObject[number]).GetComponent<ObjectInfo>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }


    public ObjectInfo GetObject(int number)
    {
        if (poolingObject.Count > 0)
        {
            var obj = poolingObject.Dequeue();
            obj.gameObject.SetActive(true);
            obj.GetComponent<ObjectInfo>().Initialization();
            return obj;
        }
        else
        {
            var newObj = CreateNewObject(number);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            newObj.GetComponent<ObjectInfo>().Initialization();
            return newObj;
        }
    }

    public void ReturnObject(ObjectInfo obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        
        poolingObject.Enqueue(obj);
        m_waveObjectCount--;
    }

    public void CreateObject()
    {

        makeObject = MakeObject();

        StartCoroutine(makeObject);
       
    }

    private IEnumerator MakeObject()
    {   
        m_isCreateEnd = false;

        transform.position = defaultPos;

        obj_objectBox.transform.position = defaultPos;

        int maxCount = Random.Range(m_MinObjectCount, m_MaxObjectCount);

        float createPos = transform.position.y;

        for (int i = 0; i < maxCount; i++) {

            yield return YieldCache.WaitForSeconds(m_createDelay);

            var fallingObject = GetObject(Random.Range(0, obj_fallingObject.Length));
            
            fallingObject.transform.parent = obj_objectBox.transform;
            fallingObject.transform.position = new Vector2(0,createPos);
            m_waveObjectCount++;
            createPos += fallingObject.m_objectLength;

        }

        m_isCreateEnd = true;

        makingDelay = MakeObjectWaveDelay();

        StartCoroutine(makingDelay);
    }

    private IEnumerator MakeObjectWaveDelay()
    {
        yield return new WaitUntil(() => m_waveObjectCount <= 0);

        yield return YieldCache.WaitForSeconds(m_createTurnDelay);

        CreateObject();

    }

    public void SoundPlayObject(AudioClip clip)
    {
        acObject.Stop();
        acObject.clip = clip;
        acObject.Play();
    }
}
