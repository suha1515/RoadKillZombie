using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum day { MORNING = 0, EVENING, NIGHT }

public class LKZ_GameManager : MonoBehaviour
{
    private static LKZ_GameManager _instance = null;
    public static LKZ_GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(LKZ_GameManager)) as LKZ_GameManager;
                if (_instance == null)
                {
                }
            }
            return _instance;
        }
    }

    day gameDay;

    public float gameLevel = 0; //게임 레벨을 정하는 변수
    public float morningTime;   //아침의 시간을 정하는 변수
    public float eveningTime;   //오후의 시간을 정하는 변수
    public float nightTime;     //밤의 시간을 정하는 변수

    public List<GameObject> stick_zombie; //버스에 달라붙은 좀비의 갯수. (조준모드로 바꿀시 없애도 될듯.)

    public bool isChangeday = false; //아침,오후,밤 전환시 사용되는 변수
    public bool isNight = false;     //밤시간을 체크하는 변수
    public float changeTime;        // 전환 시간을 정하는 변수
    public bool isStart = false;

    [Range(0.5f, 2.0f)]
    public float spawnTime = 0.0f;  //좀비의 스폰 딜레이
    [Range(1, 100)]
    public int spawnCount = 1;      //좀비의 스폰 수
    private float timer = 0.0f;     //타이머 함수를 위한 변수

    public float bus_speed = 80f;     //버스 스피드

    [Header("Weather")]
    public GameObject[] weather;
    public UILabel dayLabel;
    int dayNum = 1;

    /*민창기: 추가한 변수 11.04 11:00*/
    private int fuel = 0;
    public int Fuel
    {
        get
        {
            return fuel;
        }
        set
        {
            if (value > 99999)
            {
                fuel = 99999;
            }
            else if (value < 0)
            {
                fuel = 0;
            }
            else
                fuel = value;
        }
    }

    private int gold = 0;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            if (value > 99999)
            {
                gold = 99999;
            }
            else if (value < 0)
            {
                gold = 0;
            }
            else
                gold = value;
        }
    }

    /*민창기: 여기까지*/

    void Start()
    {
        gameDay = day.MORNING;
        stick_zombie = new List<GameObject>();
    }

    void Update()
    {
        if (gameDay == day.NIGHT)
        {
            isNight = true;
        }
        else
        {
            isNight = false;
        }

        /*
         * 스위치 문을 통하여 3가지 시간대를 전환함.
         * 
         * 아래 isChangeday를 검사하는 조건문은 아침,오후,저녁이 바뀔때 사용됨
         * 
         * (앞으로 기획하는 방향에서 바뀔것으로 예상)
         **/
        if (!isChangeday)
        {
            switch (gameDay)
            {
                case day.MORNING:
                    {
                        spawnTime = 2.0f;
                        if (Timer(morningTime))
                        {
                            isChangeday = true;
                            gameDay = day.EVENING;
                            WeatherChange(1);
                        }
                    }
                    break;
                case day.EVENING:
                    {
                        spawnTime = 1.25f;
                        if (Timer(eveningTime))
                        {
                            isChangeday = true;
                            gameDay = day.NIGHT;
                            WeatherChange(2);
                        }
                    }
                    break;
                case day.NIGHT:
                    {
                        spawnTime = 0.5f;
                        if (Timer(nightTime))
                        {
                            GameLeveling();
                            isChangeday = true;
                            gameDay = day.MORNING;
                            WeatherChange(0);

                            dayNum += 1;
                            dayLabel.text = "DAY " + dayNum;
                        }
                    }
                    break;
            }
        }
        if (isChangeday)
        {
            if (Timer(changeTime))
            {
                isChangeday = false;
            }
        }
    }
    //간단한 게임 레벨링 함수. 레벨디자인하면 없어질것.
    public void GameLeveling()
    {
        spawnCount += 2;
        gameLevel++;
        if (morningTime > 5)
            morningTime -= 1 * gameLevel;
        eveningTime += 1.2f * gameLevel;
        nightTime += 1.5f * gameLevel;

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



    public void WeatherChange(int num)
    {
        for (int i = 0; i < weather.Length; i++)
        {
            if (i == num)
                weather[i].SetActive(true);
            else
                weather[i].SetActive(false);
        }
    }
}
