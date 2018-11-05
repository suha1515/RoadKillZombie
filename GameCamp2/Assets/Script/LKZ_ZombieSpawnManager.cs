using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKZ_ZombieSpawnManager : MonoBehaviour
{
    private static LKZ_ZombieSpawnManager _instance;
    public static LKZ_ZombieSpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(LKZ_ZombieSpawnManager)) as LKZ_ZombieSpawnManager;
                if (_instance == null)
                {
                    Debug.LogError("There's no active Manager");
                }
            }
            return _instance;
        }
    }

    public List<Transform> SpawnPositions;
    public List<LKZ_ZombieWalk> ZombieList;
    public List<GameObject> ZombiePool = new List<GameObject>(600);

    private int ZombieCount;
    private float timer = 0.0f;

    private void Start()
    {
        ZombieCount = 600;
        ZombiePoolCreate(ZombieCount, ZombieList);
    }

    // Update is called once per frame
    private void Update()
    {
        //if (um.isStart && Timer(LKZ_GameManager.Instance.spawnTime))
        //{
        //    if(!LKZ_GameManager.Instance.isChangeday)
        //        ZombieSpawn();
        //}
    }

    public void ZombieSpawn(int spawnCount)
    {
        RoundManager.remainZombie += spawnCount;
        int _SpawnCount = spawnCount;
        for (int i = 0; i < ZombiePool.Count; ++i)
        {
            if (!ZombiePool[i].activeSelf)
            {
                ZombiePool[i].SetActive(true);
                ZombiePool[i].GetComponent<LKZ_ZombieWalk>().state = ZOMBIE_STATE.SPHONE;
                ZombiePool[i].GetComponent<LKZ_ZombieWalk>().Init();
                ZombiePool[i].transform.position = new Vector2(Random.Range(9.0f, 12.0f), Random.Range(-4.4f, -1.6f));
                --_SpawnCount;
                if (_SpawnCount <= 0)
                {
                    break;
                }
            }
        }
    }

    private void ZombiePoolCreate(int _count, List<LKZ_ZombieWalk> _zombies)
    {
        for (int i = 0; i < _count; ++i)
        {
            int rnd = Random.Range(0, _zombies.Count);
            GameObject clone = Instantiate(_zombies[rnd].gameObject);
            clone.name = _zombies[rnd].gameObject.name + i.ToString();
            clone.transform.position = SpawnPositions[0].position;
            ZombiePool.Add(clone);

            clone.SetActive(false);
        }
    }

    bool Timer(float _maxTime)
    {
        bool result = false;

        timer += Time.deltaTime;
        if (timer > _maxTime)
        {
            timer = 0.0f;
            result = true;
        }
        return result;
    }

    public GameObject ActiveZombieList()
    {
        GameObject result = null;


        return result;
    }
}
