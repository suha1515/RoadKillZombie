using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKZ_PlayerWeapon : MonoBehaviour
{
    public int CurNum;
    public GameObject CurWeapon;
    [SerializeField] SpriteRenderer WeaponSprite = null;

    [SerializeField] List<LKZ_Weapon> WeaponPool;
    [SerializeField] List<LKZ_Weapon> WeaponList;

    [SerializeField] LKZ_Player lkz_Player = null;

    [SerializeField] bool CanChange = true;
    [SerializeField] [Range(0.1f, 2.0f)] float ChangeDelayTime = 0.0f;

    public UISprite weaponImage;
    public UILabel weaponName;

    // Use this for initialization
    void Start()
    {
        lkz_Player = GetComponent<LKZ_Player>();
        CreateWeaponPool();

        CurNum = 0;

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeapon();

        switch (lkz_Player.state)
        {
            case PLAYERSTATE.IDLE:
                Idle();
                break;

            case PLAYERSTATE.RIGHT:
                Right();
                break;

            case PLAYERSTATE.LEFT:
                Left();
                break;
        }        
    }

    private void CreateWeaponPool()
    {
        for (int i = 0; i < WeaponList.Count; ++i)
        {
            WeaponPool.Add(Instantiate(WeaponList[i]));
            WeaponPool[i].name = WeaponList[i].name;
            WeaponPool[i].transform.parent = this.transform;
            WeaponPool[i].gameObject.SetActive(false);
        }      
    }

    private void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            WeaponChangeList();
        }
    }

    public void WeaponChangeList()
    {
        if (CanChange)
        {
            ++CurNum;

            if (CurNum >= WeaponPool.Count)
            {
                CurNum = 0;
            }

            switch (CurNum)
            {
                case 0:
                    weaponImage.spriteName = "Weapon_1";
                    weaponName.text = "Beretta";
                    break;
                case 1:
                    weaponImage.spriteName = "Weapon_2";
                    weaponName.text = "M4A1";
                    break;
                case 2:
                    weaponImage.spriteName = "Weapon_3";
                    weaponName.text = "USAS-12";
                    break;
            }

            GameObject tmp = CurWeapon;
            Init();
            tmp.SetActive(false);
            StartCoroutine(ChangeDelay(ChangeDelayTime));
        }
    }

    private void Init()
    {
        WeaponPool[CurNum].gameObject.SetActive(true);
        CurWeapon = WeaponPool[CurNum].gameObject;        
        WeaponSprite = CurWeapon.GetComponent<SpriteRenderer>();
        CurWeapon.GetComponent<LKZ_Weapon>().Reload();
    }


    void Idle()
    {
        WeaponSprite.flipX = true;
        WeaponPool[CurNum].transform.parent = null;
        WeaponPool[CurNum].transform.position = CurWeapon.GetComponent<LKZ_Weapon>().IdlePos;
        WeaponPool[CurNum].transform.parent = this.transform;
    }

    void Right()
    {
        WeaponSprite.flipX = true;
        WeaponPool[CurNum].transform.parent = null;
        WeaponPool[CurNum].transform.position = CurWeapon.GetComponent<LKZ_Weapon>().RightPos;
        WeaponPool[CurNum].transform.parent = this.transform;
    }

    void Left()
    {
        WeaponSprite.flipX = false;
        WeaponPool[CurNum].transform.parent = null;
        WeaponPool[CurNum].transform.position = CurWeapon.GetComponent<LKZ_Weapon>().LeftPos;
        WeaponPool[CurNum].transform.parent = this.transform;
    }

    IEnumerator ChangeDelay(float _time)
    {
        CanChange = false;
        yield return new WaitForSecondsRealtime(_time);
        CanChange = true;
    }
}
