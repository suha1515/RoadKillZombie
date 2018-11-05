using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKZ_GoldLabel : MonoBehaviour
{
    int gold;
    // Update is called once per frame
    void Update()
    {
        UpdaeGold();
    }


    void UpdaeGold()
    {
        gold = LKZ_GameManager.Instance.Gold;
        GetComponentInChildren<UILabel>().text = gold.ToString();
    }
}
