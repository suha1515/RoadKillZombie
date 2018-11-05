using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class RoundManager : MonoBehaviour {

    string filename = "roundData.json"; //라운드 정보를 가져올 json 파일이름
    string path;                        //파일 경로
    Round[] roundData;                  //json 데이터를 담을 Round 클래스 GameData.cs 참고


    public LKZ_ZombieSpawnManager zspawn;   //좀비스폰매니저접근하여 좀비스폰

    [Header("UI")]
    public UIManager um;

    List<Monster> monsterGroup = new List<Monster>();   //스폰될 좀비그룹을 저장하는 리스트
   
    public float endPoint = 750f;       //라운드의 끝(거리)
    public float startPoint = 0f;       //시작지점
    public static int remainZombie=0;   //남아있는 좀비
	// Use this for initialization
	void Start () {
        path = Application.dataPath + "/JBS/" + filename;
        ReadData();     //json에 저장된 정보를 가져온다.
        monsterGroup.AddRange(roundData[0].monster);    //가져온 정보를 list에 저장(monster 배열 ->리스트로 변환)
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.R))
        {
            //ReadData();
        }
        
    
	}
    void FixedUpdate() //0.01초
    {
        if (um.isStart)     //um 에서 게임이 시작할 경우
        {
            float speed = Converter.khToms(LKZ_GameManager.Instance.bus_speed);  //버스의 속도는 Converter 클래스의 khToms( 시속을 초속으로 바꿔주는 함수) 에들어가 현재 버스의 초속으로 변환된다.
            startPoint += speed/100;                                            //초속만큼 시작지점에 더해짐
            for (int i =0;i<monsterGroup.Count;i++)                             //스폰할 몬스터그룹의 정보를 검사함.
            {
                ///115
                if(monsterGroup[i].SpawnPoint<startPoint)                       //리스트 MonsterGroup에 담겨있는 Monster 클래스 정보중 startPoint( 스폰될 위치) 를 검사하여 현재 지나온 거리보다 작을 경우(지나친경우)
                {
                    zspawn.ZombieSpawn(monsterGroup[i].count);                  //좀비스폰매너지의 좀비스폰 Monster클래스 정보중 count (좀비 개수)만큼 소환한다.
                    monsterGroup.RemoveAt(i);                                   //소환된 좀비 그룹은 리스트에서 제거한다.
                }
            }
            if(monsterGroup.Count==0&&remainZombie==0)                          //리스트도 0 이고 현재 존재하는 좀비도0일 경우 게임 종료
            {
                um.GameOver();
            }
            
        }
    }
    //몬스터 그룹을 바꾸는 함수. (라운드마다)
   public void ChangeMosnter(string _stageName)
    {
        for(int i=0;i<roundData.Length;i++)
        {
            if(roundData[i].roundName==_stageName)
            {
                monsterGroup.AddRange(roundData[i].monster);
            }
        }
    }
    void SaveData()
    {  
    }
    void ReadData()
    {
        string contents = System.IO.File.ReadAllText(path);                 //path 경로에있는 파일을 파일시스템을 이용하여 text형식의 데이터로 읽는다 (json 이 텍스트형식)
        roundData = JsonHelper.FromJson<Round>(contents);                   //json에서 객체로 전환해준다. JsonHelper는 직접만든 wrapper 클래스로 GameData.cs 참고
        for(int i=0;i<roundData[0].waveCount;i++)                           //정보확인용 
        {
            Debug.Log(roundData[0].monster[i].count);
        }
    }
}
