using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKZ_StagePointManager : MonoBehaviour
{
    Vector3 mousePos = new Vector3();
    [SerializeField] LayerMask ExclusiveLayers;
    //스테이지 포인트 오브젝트의 이름을 담을 문자열 -> 이걸로 스테이지 정보를 받아오면 됨
    public string SelectedStage = null;
    static GameObject curStage = null;
    private void Update()
    {
        //PC 및 에디터에서 클릭 업 시
        if (Input.GetMouseButtonUp(0))
        {
            mousePos = Input.mousePosition;
            SelectedStage = GetStageNameByRaycast(mousePos);
        }

        //모바일에서 터치 업 시
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            mousePos = Input.GetTouch(0).position;
            SelectedStage = GetStageNameByRaycast(mousePos);
        }
    }

    private string GetStageNameByRaycast(Vector3 _rayOrigin)
    {
        string _stage = null;
        Ray ray = Camera.main.ScreenPointToRay(_rayOrigin);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, ~ExclusiveLayers))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("StagePoint"))
            {
                if (curStage == null)
                {
                    curStage = hit.collider.gameObject;
                }
                else
                {
                    //화면이 터치는 됬는데 스테이지 포인트가 이전과 다른것 터치한 경우 
                    if (curStage != hit.collider.gameObject)
                    {
                        curStage.GetComponent<LKZ_StagePoint>().NotSelected();
                        curStage = hit.collider.gameObject;
                    }
                }
                _stage = curStage.name;
                curStage.GetComponent<LKZ_StagePoint>().OnSelected();
            }
            else
            {
                //화면이 터치는 됬는데 스테이지 포인트가 아닌 다른 것 터치한 경우 
                if (curStage != null)
                {
                    if (curStage != hit.collider.gameObject)
                    {
                        curStage.GetComponent<LKZ_StagePoint>().NotSelected();
                        curStage = null;
                        _stage = null;
                    }
                }
            }
            Debug.DrawLine(ray.origin, ray.direction);
        }
        else
        {
            //화면이 터치는 됬는데 스테이지 포인트가 아닌 다른것 터치한 경우 중 레이캐스트에 아무것도 충돌하지 않은 경우 
            //이건 쓸지 말지 고민중
            if (curStage != null)
            {
                curStage.GetComponent<LKZ_StagePoint>().NotSelected();
                curStage = null;
                _stage = null;
            }
        }

        return _stage;
    }
}

