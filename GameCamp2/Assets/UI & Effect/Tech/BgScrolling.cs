using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScrolling : MonoBehaviour {

    public float scrollSpeed;
    //스크롤할 속도를 로 지정해 줍니다.
    private Material thisMaterial;
    //Quad의 Material 데이터를 받아올 객체를 선언합니다.

    Vector2 offVec = Vector2.zero;


    void Start()
    {
        thisMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // 배경 스크롤링
        ScrollingBG();
    }

    void ScrollingBG()
    {
        // 배경 스크롤링
        offVec += new Vector2(0.1f * scrollSpeed * Time.deltaTime, 0);
        thisMaterial.SetTextureOffset("_MainTex", offVec);
    }
}
