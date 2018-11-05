using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingScript : MonoBehaviour {

    private WeaponScript weaponScript;

    // 총 발사 유무 확인
    private bool canShooting = false;

    // 총 간 delay 변수
    private float conDelay = 0;

    private Vector2 aimingVector;

    // Use this for initialization
    void Start()
    {
        weaponScript = GetComponent<WeaponScript>();

        StartCoroutine(Shooting());
    }
    // Update is called once per frame
    void Update()
    {
        // 조준
        Aiming();
    }

    // ============================================================== public funtion ================================================================




    // ============================================================== private funtion ================================================================

    IEnumerator Shooting()
    {
        while (true)
        {
            conDelay -= Time.deltaTime;

            // dealy 시간까지 대기하였으므로 발사
            if (conDelay < 0 && canShooting)
            {
                // 발사
                weaponScript.WeaponFire(aimingVector);

                // dealy 초기화
                conDelay = weaponScript.weaponPool[weaponScript.conWeapon].delay;
            }


            yield return null;
        }
    }

    // 조준 스크립트
    private void Aiming()
    {
        // 터치 or 클릭
        if (Input.GetMouseButton(0))
        {
            // 사격 가능
            canShooting = true;

            // 총을 기준으로..
            Vector2 center = weaponScript.weaponPool[weaponScript.conWeapon].transform.Find("Sprite").position;

            // 클릭 좌표가..
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); ;

            // 오른쪽이면..
            if (clickPos.x > center.x)
            {
                Vector2 temp = clickPos - center;
                aimingVector = temp;

                float dot = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;

                // 무기 회전
                weaponScript.WeaponRotate(Quaternion.Euler(0, 0, dot), true);

                // 캐릭터 자세 변환
                GetComponent<LKZ_Player>().state = PLAYERSTATE.RIGHT;
                // 변한 자세에 따른 무기 위치 변화
                weaponScript.WeaponPosChange(GetComponent<LKZ_Player>().state);
            }
            // 왼쪽이면..
            else
            {
                Vector2 temp = clickPos - center;
                aimingVector = temp;

                float dot = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;

                // 무기 회전
                weaponScript.WeaponRotate(Quaternion.Euler(0, 0, dot - 180), false);

                // 캐릭터 자세 변환
                GetComponent<LKZ_Player>().state = PLAYERSTATE.LEFT;
                // 변한 자세에 따른 무기 위치 변화
                weaponScript.WeaponPosChange(GetComponent<LKZ_Player>().state);
            }

            // 발사 가능하다면 발사
        }
        else
        {
            // 무기 회전
            weaponScript.WeaponRotate(Quaternion.Euler(0, 0, 0), true);

            // 캐릭터 자세 변환
            GetComponent<LKZ_Player>().state = PLAYERSTATE.IDLE;
            // 변한 자세에 따른 무기 위치 변화
            weaponScript.WeaponPosChange(GetComponent<LKZ_Player>().state);

            // 사격 불가능
            canShooting = false;
        }
    }
}
