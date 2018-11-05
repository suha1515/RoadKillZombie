using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKZ_StagePointText : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        GetStageNameText();
    }

    void GetStageNameText()
    {
        GetComponent<TextMesh>().text = transform.parent.name;
    }
}
