using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
    
    public List<WeaponInfo> weaponPool;
    public int conWeapon = 0;

    public GameObject bulletSample;
    private List<BulletScript> bulletPool;
    public int bulletCount;

    // 격발 시 빛 효과 담당 코루틴
    private Coroutine FireLightCoroutine;

    // Use this for initialization
    void Start () {
        bulletPool = new List<BulletScript>();
        for (int i = 0; i < bulletCount; i++)
        {
            bulletPool.Add(Instantiate<GameObject>(bulletSample).GetComponent<BulletScript>());
        }

    }
	
	// Update is called once per frame
	void Update () {
        // 키보드 상 무기 교체 버튼 클릭 확인 후 무기 교체
        ChangeWeapon();

    }



    // ============================================================== public funtion ================================================================

    // 무기 교체
    public void WeaponChangeList()
    {
        // 기존에 사용하는 무기를 지우고..
        weaponPool[conWeapon].gameObject.SetActive(false);
        // 기존에 사용하던 무기의 rotation 값을 저장한 뒤, 초기화
        Quaternion temp =  weaponPool[conWeapon].transform.Find("Sprite").rotation;
        weaponPool[conWeapon].transform.Find("Sprite").rotation = new Quaternion(0, 0, 0, 0);

        conWeapon++;

        // (단, 무기 최대 갯수를 넘어가면 시작 무기를 가지며..)
        if (conWeapon == weaponPool.Count)
        {
            conWeapon = 0;

        }

        // 이전 총의 회전값 그대로 새로운 무기를 장착
        weaponPool[conWeapon].transform.Find("Sprite").rotation = temp;
        weaponPool[conWeapon].gameObject.SetActive(true);
    }

    // 캐릭터 상태 변경에 따른 무기 위치 변경
    public void WeaponPosChange(PLAYERSTATE state)
    {
        switch(state)
        {
            case PLAYERSTATE.IDLE:
                weaponPool[conWeapon].transform.localPosition = weaponPool[conWeapon].idlePos;
                break;
            case PLAYERSTATE.LEFT:
                weaponPool[conWeapon].transform.localPosition = weaponPool[conWeapon].leftPos;
                break;
            case PLAYERSTATE.RIGHT:
                weaponPool[conWeapon].transform.localPosition = weaponPool[conWeapon].rightPos;
                break;
        }
    }

    // 무기 회전
    public void WeaponRotate(Quaternion newRotation, bool flipX)
    {
        // 총 이미지를 회전
        weaponPool[conWeapon].transform.Find("Sprite").rotation = newRotation;
        weaponPool[conWeapon].transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX = flipX;


        // 오른쪽 사격에서의 Light 회전
        if(flipX)
        {
            weaponPool[conWeapon].fireLight.transform.localPosition = new Vector2(weaponPool[conWeapon].fireLightVector.x * -1, weaponPool[conWeapon].fireLightVector.y);
            weaponPool[conWeapon].fireLight.GetComponent<SpriteRenderer>().flipX = flipX;

            Vector3 temp = weaponPool[conWeapon].fireLight.transform.localPosition;
            weaponPool[conWeapon].fireFlash.transform.localPosition = new Vector3(temp.x + 0.3f, temp.y, weaponPool[conWeapon].fireFlash.transform.localPosition.z);
        }
        // 왼쪽 사격에서의 Light 회전
        else
        {
            weaponPool[conWeapon].fireLight.transform.localPosition = weaponPool[conWeapon].fireLightVector;
            weaponPool[conWeapon].fireLight.GetComponent<SpriteRenderer>().flipX = flipX;

            //weaponPool[conWeapon].fireFlash.transform.localPosition = new Vector2(weaponPool[conWeapon].fireFlashVector.x * -1, weaponPool[conWeapon].fireFlashVector.y);
            Vector3 temp = weaponPool[conWeapon].fireLight.transform.localPosition;
            weaponPool[conWeapon].fireFlash.transform.localPosition = new Vector3(temp.x - 0.3f, temp.y, weaponPool[conWeapon].fireFlash.transform.localPosition.z);
        }
    }

    // 지정된 방향으로 총 발사
    public void WeaponFire(Vector2 vector)
    {
        // 일정 시간 fireLight 표시
        if (FireLightCoroutine != null)
            StopCoroutine(FireLightCoroutine);
        FireLightCoroutine = StartCoroutine(FireLighting());

        // 총알 생성
        Fire(vector);
    }


    // ============================================================== private funtion ================================================================
    
    // 키보드 상 무기 교체 버튼 클릭 확인
    private void ChangeWeapon()
    {
        // Q를 누르면 ..
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 무기 교체
            WeaponChangeList();
        }
    }

    // 효과 표시
    IEnumerator FireLighting()
    {
        weaponPool[conWeapon].fireFlash.SetActive(true);
        weaponPool[conWeapon].fireLight.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        weaponPool[conWeapon].fireLight.SetActive(false);
        weaponPool[conWeapon].fireFlash.SetActive(false);
    }

    private void Fire(Vector2 vector)
    {
        switch(weaponPool[conWeapon].kind)
        {
            // 권총은 한 방향으로 한 개의 총알을 생성
            case WeaponInfo.Kind.Pistol:
                for(int i = 0; i < bulletCount; i++)
                {
                    // 사용하지 않는 총알을 찾아
                    if(bulletPool[i].gameObject.activeSelf == false)
                    {
                        // 총알 초기 위치 지정
                        bulletPool[i].initPos = weaponPool[conWeapon].fireLight.transform.position;

                        // 총알 날아가는 방향 지정
                        bulletPool[i].moveVector = vector;

                        // 총알 속도 지정
                        bulletPool[i].speed = 1;

                        // 총알 거리 지정
                        bulletPool[i].distance = 20;

                        bulletPool[i].gameObject.SetActive(true);

                        break;
                    }
                }
                break;
            // 기관총은 한 방향으로 여러 개의 총알을 생성
            case WeaponInfo.Kind.Machine_gun:

                break;
           // 샷건은 여러 방향으로 여러 개의 총알을 생성
            case WeaponInfo.Kind.Shotgun:

                break;
        }
    }
}
