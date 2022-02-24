using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectpoolManager : MonoBehaviour
{
    public static ObjectpoolManager Instance;
    [SerializeField] 
    public GameObject poolingObjectPrefab1;
    public GameObject poolingObjectPrefab2;
    public Transform ObjectpoolPos1;
    public Transform ObjectpoolPos2;
    Queue<Monster> poolingObjectQueue1 = new Queue<Monster>();
    Queue<Monster> poolingObjectQueue2 = new Queue<Monster>();
    public float SpawnDelay;
    [SerializeField]
    public int MAX_MONSTER_CNT;

    IEnumerator setSpawnDelay(float delayTime)
    {
        Debug.Log("spawn delay " + delayTime + " time");
        yield return new WaitForSeconds(delayTime);

    }
    private void Awake() {
        Instance = this;
        Initialize(MAX_MONSTER_CNT);
    }
    private void Initialize(int initCount){
        for(int i = 0; i < initCount; i++) { 
            poolingObjectQueue1.Enqueue(CreateNewObject(poolingObjectPrefab1));
            poolingObjectQueue2.Enqueue(CreateNewObject(poolingObjectPrefab2));
        }
    }

    private Monster CreateNewObject(GameObject poolingObjectPrefab)
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Monster>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public Monster SpawnMonster(Queue<Monster> poolingObjectQueue, int poolidx) { 
        if(poolingObjectQueue.Count > 0) { 
            var obj = poolingObjectQueue.Dequeue(); 
            obj.transform.SetParent(transform); 
            obj.gameObject.transform.position = RandomPosition(poolidx);
            obj.gameObject.SetActive(true);
            return obj; 
        } 
        else { 
            return null; 
        } 
    }
    public Vector3 RandomPosition(int poolidx)
    {
        Vector3 basePos = new Vector3 (0,0,0);
        if (poolidx == 0)
        {
            basePos = ObjectpoolPos1.position;
        }
        else if (poolidx == 1)
        {
            basePos = ObjectpoolPos2.position;
        }
        float randomX = Random.Range(-3f, 3f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
        float randomZ = Random.Range(-3f, 3f); //적이 나타날 Z좌표를 랜덤으로 생성해 줍니다.
        float posX = basePos.x + randomX;
        float posY = basePos.y;
        float posZ = basePos.z + randomZ;
        Vector3 SpawnPos = new Vector3(posX, posY, posZ);
        
        return SpawnPos;
    } 
    public void ReturnObject(Monster obj) {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        if(obj.PoolingId == 0) // poolingObjectQueue1
        {
            Instance.poolingObjectQueue1.Enqueue(obj);
        }
       else if(obj.PoolingId == 1)
        {
            Instance.poolingObjectQueue2.Enqueue(obj);
        }
        
    }
    private void Start()
    {
        StartCoroutine("Spawn",10.0f);
    }
    private void Update() {

    }
    
    IEnumerator Spawn(float delayTime){
        while (Instance.poolingObjectQueue1.Count > 0)
        {
            SpawnMonster(poolingObjectQueue1, 0);  
        }
        while (Instance.poolingObjectQueue2.Count > 0)
        {
            SpawnMonster(poolingObjectQueue2, 1);
        }
        yield return new WaitForSeconds(delayTime);
        StartCoroutine("Spawn", delayTime);
    }
}
