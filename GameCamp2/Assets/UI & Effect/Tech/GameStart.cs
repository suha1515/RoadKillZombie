using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

    // 자동차
    public GameObject car;

    // 시간 manager
    public GameObject timeManager;

    // bg manager
    public GameObject bgManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator StartCoroutine()
    {
        yield return null;
    }
}
