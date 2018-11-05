using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    // 총알 생성 위치
    [HideInInspector] public Vector2 initPos;
    // 총알 이동 방향
    [HideInInspector] public Vector2 moveVector;
    // 총알 이동 거리, 기본 값 20
    public float distance = 20;
    // 총알 이동 속도, 기본 값 1
    public float speed = 0.01f;
    // 총알의 데미지
    public float damage = 2f;

    // 자주 쓰는 Component 캐싱
    private Transform trans;

    // Use this for initialization
    void Start () {
        // Transfrom Component 캐싱
        trans = GetComponent<Transform>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 이동
        trans.position = initPos + moveVector * speed;

        // 최대 거리를 이동한 총알은..
        if (Vector2.Distance(trans.position, initPos + moveVector * distance) < 0.1f)
        {
            // 정보 초기화
            initPos = Vector2.zero;
            moveVector = Vector2.zero;
            distance = 0;
            speed = 0;
            
            // 이동 제한
            gameObject.SetActive(false);
        }
    }
}
