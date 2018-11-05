using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Monster
{
    /*
    * 몬스터 클래스
    * 용도: 몬스터의 정보를 담음 (그룹별로 지정) 
    */
    public string name; //몬스터의 종류
    public int count;   //몬스터 갯수
    public string SpwanDirection;   //소환될 방향
    public float SpawnPoint;        //소환될 지점


}
[System.Serializable]
public class Round  {
    /*
    * 라운드 클래스
    * 용도: 라운드의 정보를 담음 , 몬스터 배열과 함께(라운드에 소환될 몬스터) 
    */
    public string roundName;        //라운드의 이름
    public int waveCount;           //웨이브 카운트 즉 몬스터 그룹의 갯수와 같음.
    public Monster[] monster;       //몬스터 배열 
}

public static class JsonHelper
{
   /*
    * JsonUtility 헬퍼 클래스 
    * 용도: Json 은 객체 배열을 저장할수 없다는 단점이 있어서 제작
    * 사용법: FronJson <-Json 에서 객체정보로 변환
    *         ToJson <-객체정보를 Json 정보로 변환
    */
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);    //json 정보를 wrapper 클래스에 저장
        return wrapper.items;                                           //리턴
    }
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;                                         //저장하고자 하는 배열정보를 wrapper 객체에 저장한후
        return JsonUtility.ToJson(wrapper);                            //wrapper 객체를 json으로 변환
    }
    public static string ToJson<T>(T[] array,bool prettyPrint)         //위와 용도는 똑같으나 true,false 값도 같이 변환해줌.
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
    [System.Serializable]
    private class Wrapper<T>                                           //사실 위에 함수는 이해할 필요없는 래핑이고 이 클래스가 배열정보를 저장할수있게 도와줌
    {                                                                  //이 클래스를 여러개 저장할 수는 없다는뜻.
        public T[] items;
    }
}
