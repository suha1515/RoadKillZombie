using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYERSTATE { IDLE = 0, RIGHT, LEFT }

public class LKZ_Player : MonoBehaviour
{
    private AudioSource shooting;

    public List<AudioClip> shootingClip = new List<AudioClip>();

    GameObject bulletPrefab;
    LKZ_PlayerWeapon lkz_PlayerWeapon;
    const float shootDelay = 0.05f;
    float shootTimer = 0;

    public float idleBackTime = 0.0f;
    float timer = 0.0f;
    public PLAYERSTATE state;
    public UIManager um;

    [SerializeField] GameObject[] Light = null;

    // Use this for initialization
    void Start()
    {
        state = PLAYERSTATE.IDLE;
        shooting = gameObject.GetComponent<AudioSource>();
        lkz_PlayerWeapon = GetComponent<LKZ_PlayerWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        zombie_Attack();  
    }


    private void zombie_Attack()
    {
        /*
        if (state != PLAYERSTATE.IDLE)
        {
            timer += Time.deltaTime;
            if (timer > idleBackTime)
                state = PLAYERSTATE.IDLE;
        }
        
        if (Application.platform == RuntimePlatform.Android)
        {
            Vector3 touchPos = Input.GetTouch(0).position;

            if (um.isStart && touchPos.x >= Screen.width / 2 && shootTimer > shootDelay)
            {
                state = PLAYERSTATE.RIGHT;
                timer = 0.0f;
                lkz_PlayerWeapon.CurWeapon.GetComponent<LKZ_Weapon>().um = um;
                lkz_PlayerWeapon.CurWeapon.GetComponent<LKZ_Weapon>().RightAttack();
                shootTimer = 0;
                shooting.clip = shootingClip[0];
                shooting.Play();
                StartCoroutine(MuzzleFlash(Light[0]));

            }
            else if (um.isStart && touchPos.x <= Screen.width / 2 && shootTimer > shootDelay)
            {
                state = PLAYERSTATE.LEFT;
                timer = 0.0f;
                lkz_PlayerWeapon.CurWeapon.GetComponent<LKZ_Weapon>().LeftAttack();
                shootTimer = 0;
                shooting.clip = shootingClip[0];
                shooting.Play();
                StartCoroutine(MuzzleFlash(Light[1]));
            }
        }
        else
        {
            if (um.isStart && (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L)) == true && shootTimer > shootDelay)
            {
                state = PLAYERSTATE.RIGHT;
                timer = 0.0f;
                lkz_PlayerWeapon.CurWeapon.GetComponent<LKZ_Weapon>().um = um;
                lkz_PlayerWeapon.CurWeapon.GetComponent<LKZ_Weapon>().RightAttack();
                shootTimer = 0;
                shooting.clip = shootingClip[lkz_PlayerWeapon.CurNum];
                shooting.Play();
                StartCoroutine(MuzzleFlash(Light[0]));

            }
            else if (um.isStart && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)) == true && shootTimer > shootDelay)
            {
                state = PLAYERSTATE.LEFT;
                timer = 0.0f;
                lkz_PlayerWeapon.CurWeapon.GetComponent<LKZ_Weapon>().LeftAttack();
                shootTimer = 0;
                shooting.clip = shootingClip[lkz_PlayerWeapon.CurNum];
                shooting.Play();
                StartCoroutine(MuzzleFlash(Light[1]));
            }
        }

        shootTimer += Time.deltaTime;
        */
    }

    IEnumerator MuzzleFlash(GameObject _light)
    {
        _light.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        _light.SetActive(false);
    }
}
