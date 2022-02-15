using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectpoolManager : MonoBehaviour
{
    public static ObjectpoolManager Instance;
    [SerializeField] 
    private GameObject poolingObjectPrefab;
    Queue<Monster> poolingObjectQueue = new Queue<Monster>();
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
            poolingObjectQueue.Enqueue(CreateNewObject()); 
        }
    }

    private Monster CreateNewObject(){
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Monster>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public Monster SpawnMonster() { 
        if(Instance.poolingObjectQueue.Count > 0) { 
            var obj = Instance.poolingObjectQueue.Dequeue(); 
            obj.transform.SetParent(transform); 
            obj.gameObject.transform.position = RandomPosition();
            obj.gameObject.SetActive(true);
            return obj; 
        } 
        else { 
            return null; 
        } 
    }
    public Vector3 RandomPosition(){
        Vector3 basePos = this.transform.position;
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
        Instance.poolingObjectQueue.Enqueue(obj); 
    }
    private void Start()
    {
        StartCoroutine("Spawn",10.0f);
    }
    private void Update() {

    }
    
    IEnumerator Spawn(float delayTime){
        while (Instance.poolingObjectQueue.Count > 0)
        {
            SpawnMonster();  
        }
        yield return new WaitForSeconds(delayTime);
        StartCoroutine("Spawn", 10.0f);
    }
}
