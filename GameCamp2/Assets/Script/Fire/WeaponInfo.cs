using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour {

    public enum Kind { Pistol, Machine_gun , Shotgun };
    public Kind kind;

    public Vector2 idlePos;
    public Vector2 leftPos;
    public Vector2 rightPos;

    // 총알 발사 delay
    public float delay;

    // 총알 최대 사정거리
    public float distance;
    // 총알 이동 속도, 기본 값 1
    public float speed = 1;

    // light Sprite
    [HideInInspector]
    public GameObject fireLight;
    [HideInInspector]
    public Vector2 fireLightVector;

    // light flash
    [HideInInspector]
    public GameObject fireFlash;
    public Vector3 fireFlashVector;

    private void Start()
    {
        fireLight = transform.Find("Sprite").Find("Light").gameObject;
        fireLightVector = transform.Find("Sprite").Find("Light").localPosition;

        fireFlash = transform.Find("Sprite").Find("Flash").gameObject;
        fireFlashVector = transform.Find("Sprite").Find("Flash").localPosition;
    }

}
