using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKZ_StagePoint : MonoBehaviour
{
    public string StageName = null;
    public bool isOpen = false;
    [SerializeField] bool isSelected = false;
    [SerializeField] bool isClear = false;

    [SerializeField] LKZ_StagePoint NextStage;

    private void Awake()
    {
        StageName = gameObject.name;
    }

    //스테이지를 클리어 하면 쓰는 함수. 다음 스테이지가 오픈된다.
    public void StageClear()
    {
        isClear = true;
        NextStage.isOpen = true;
    }

    //스테이지 포인트가 선택해제 되면 쓰는 함수
    public void NotSelected()
    {
        isSelected = false;
        Debug.Log("Not Selected : " + StageName);
    }

    //스테이지 포인트가 선택되면 쓰는 함수
    public void OnSelected()
    {
        isSelected = true;
        Debug.Log("Selected : " + StageName);
        //이제 여기에 해당 스테이지를 불러오는 코드를 넣어야 함.
        //혹은 게임 시작전 정비 할수 있는 UI를 불러오거나
    }
}
