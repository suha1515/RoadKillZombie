using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LZK_FuelLabel : MonoBehaviour {

    int fuel;
    // Update is called once per frame
    void Update()
    {
        UpdateFuel();
    }


    void UpdateFuel()
    {
        fuel = LKZ_GameManager.Instance.Fuel;
        GetComponentInChildren<UILabel>().text = fuel.ToString();
    }
}
