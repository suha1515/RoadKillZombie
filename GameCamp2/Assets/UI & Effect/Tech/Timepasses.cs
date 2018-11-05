using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timepasses : MonoBehaviour {

    public Light Directlight;
    struct timeRGB
    {
        public float r;
        public float g;
        public float b;
    }

    timeRGB morning;
    timeRGB afternoon;
    timeRGB night;

    timeRGB conRGB;

    [HideInInspector] public float conTime = 0;
    float maxTime;
    public float timeSpeed;

    // Use this for initialization
    void Start ()
    {
        morning.r = 255;
        morning.g = 255;
        morning.b = 255;

        afternoon.r = 255;
        afternoon.g = 180;
        afternoon.b = 180;

        night.r = 20;
        night.g = 20;
        night.b = 20;

        // 최초 아침으로 설정
        conRGB = morning;
        maxTime = LKZ_GameManager.Instance.changeTime * 3;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // 시간의 흐름
        if (LKZ_GameManager.Instance.isChangeday)
        {
            conTime += Time.deltaTime;
        }

        if (conTime > maxTime)
        {
            conTime = 0;
        }

        // 현재 시간에 맞는 색 저장
        if (conTime < LKZ_GameManager.Instance.changeTime)
        {
            conRGB.r = morning.r - ((morning.r - afternoon.r) * (conTime / LKZ_GameManager.Instance.changeTime));
            conRGB.g = morning.g - ((morning.g - afternoon.g) * (conTime / LKZ_GameManager.Instance.changeTime));
            conRGB.b = morning.b - ((morning.b - afternoon.b) * (conTime / LKZ_GameManager.Instance.changeTime));
        }
        else if (conTime < LKZ_GameManager.Instance.changeTime * 2)
        {
            conRGB.r = afternoon.r - ((afternoon.r - night.r) * (conTime - LKZ_GameManager.Instance.changeTime) / LKZ_GameManager.Instance.changeTime);
            conRGB.g = afternoon.g - ((afternoon.g - night.g) * (conTime - LKZ_GameManager.Instance.changeTime) / LKZ_GameManager.Instance.changeTime);
            conRGB.b = afternoon.b - ((afternoon.b - night.b) * (conTime - LKZ_GameManager.Instance.changeTime) / LKZ_GameManager.Instance.changeTime);
        }
        else
        {
            conRGB.r = night.r - ((night.r - morning.r) * (conTime - LKZ_GameManager.Instance.changeTime * 2) / LKZ_GameManager.Instance.changeTime);
            conRGB.g = night.g - ((night.g - morning.g) * (conTime - LKZ_GameManager.Instance.changeTime * 2) / LKZ_GameManager.Instance.changeTime);
            conRGB.b = night.b - ((night.b - morning.b) * (conTime - LKZ_GameManager.Instance.changeTime * 2) / LKZ_GameManager.Instance.changeTime);
        }
        Color color = new Color(conRGB.r / 255, conRGB.g / 255, conRGB.b / 255);

        // 각 객체의 바탕색을 저장한 색으로 변경
        Directlight.color = color;     
    }  
}
