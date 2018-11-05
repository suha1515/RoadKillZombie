using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKZ_Weapon : MonoBehaviour
{
    public UIManager um;
    public Vector3 IdlePos;
    public Vector3 RightPos;
    public Vector3 LeftPos;

    [SerializeField] bool CanShoot;
    [SerializeField] [Range(1, 3)] int WeaponDamage = 0;
    [SerializeField] [Range(0.0f, 2.0f)] float WeaponSpeed = 0.0f;
    [SerializeField] [Range(0, 7)] float WeaponRange = 0.0f;
    [SerializeField] [Range(1, 5)] int TargetCount = 0;
    [SerializeField] [Range(1, 15)] int ShootCount = 0;

    public bool Reloading = false;
    [SerializeField] [Range(0.0f, 5.0f)] float WeaponReload = 0.0f;
    [SerializeField] [Range(1, 30)] int WeaponMagazine = 0;
    int curMagazine;

    float timer = 0.0f;
    float delay = 0.0f;

    [SerializeField] GameObject[] Light = null;

    private void Update()
    {
        if (!CanShoot)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                Reloading = false;
                CanShoot = true;
                timer = 0.0f;
                Debug.Log(curMagazine.ToString() + CanShoot.ToString());
            }
        }
    }

    public void Reload()
    {
        Reloading = true;
        curMagazine = WeaponMagazine;
    }

    public void RightAttack()
    {
        StartCoroutine(MuzzleFlash(Light[0]));
        if (CanShoot)
        {
            List<GameObject> tmp = new List<GameObject>();
            for (int i = 0; i < LKZ_ZombieSpawnManager.Instance.ZombiePool.Count; ++i)
            {
                if (LKZ_ZombieSpawnManager.Instance.ZombiePool[i].activeSelf == true
                    && WeaponRange > LKZ_ZombieSpawnManager.Instance.ZombiePool[i].transform.position.x)
                {
                    if (LKZ_ZombieSpawnManager.Instance.ZombiePool[i].GetComponent<LKZ_ZombieWalk>().state == ZOMBIE_STATE.WALK)
                    {
                        tmp.Add(LKZ_ZombieSpawnManager.Instance.ZombiePool[i]);
                    }
                }
            }
            Debug.Log(tmp.Count);

            if (tmp.Count < TargetCount)
            {
                for (int i = 0; i < tmp.Count; ++i)
                {
                    for(int j =0; j < ShootCount; ++j)
                    {
                        if(tmp[i].activeSelf == false)
                        {
                            break;
                        }
                        else
                        {
                            um.GetPoint();
                            tmp[i].GetComponent<LKZ_ZombieWalk>().em.EffectInstance();
                            tmp[i].GetComponent<LKZ_ZombieWalk>().Heath -= WeaponDamage;
                        }
                       
                    }
           
                }

            }
            else 
            {
                for (int i = 0; i < TargetCount; ++i)
                {
                    if (tmp[i].activeSelf == false)
                    {
                        break;
                    }
                    else
                    {
                        um.GetPoint();
                        tmp[i].GetComponent<LKZ_ZombieWalk>().em.EffectInstance();
                        tmp[i].GetComponent<LKZ_ZombieWalk>().Heath -= WeaponDamage;
                    }
                }
            }
            tmp.Clear();

            --curMagazine;
            CanShoot = false;

            if (curMagazine <= 0)
            {
                delay = WeaponReload;
                Reload();
            }
            else
            {
                delay = WeaponSpeed;
            }

            Debug.Log(curMagazine.ToString() + CanShoot.ToString());
        }
    }


    public void LeftAttack()
    {
        StartCoroutine(MuzzleFlash(Light[1]));
        if (CanShoot)
        {
            int k = 0;
            for (int j = 0; j < LKZ_ZombieSpawnManager.Instance.ZombiePool.Count; ++j)
            {
                if (LKZ_ZombieSpawnManager.Instance.ZombiePool[j].activeSelf == true)
                {
                    if (LKZ_ZombieSpawnManager.Instance.ZombiePool[j].GetComponent<LKZ_ZombieWalk>().state == ZOMBIE_STATE.HANG)
                    {
                        if (k < TargetCount)
                        {
                            ++k;
                            um.GetPoint();
                            LKZ_ZombieSpawnManager.Instance.ZombiePool[j].GetComponent<LKZ_ZombieWalk>().em.EffectInstance();

                            LKZ_ZombieSpawnManager.Instance.ZombiePool[j].GetComponent<LKZ_ZombieWalk>().Heath -= WeaponDamage;
                            --curMagazine;
                            CanShoot = false;

                            if (curMagazine <= 0)
                            {
                                delay = WeaponReload;
                                Reload();
                            }
                            else
                            {
                                delay = WeaponSpeed;
                            }
                            Debug.Log(curMagazine.ToString() + CanShoot.ToString());
                        }
                    }
                }
            }

        }
    }



    IEnumerator MuzzleFlash(GameObject _light)
    {
        _light.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        _light.SetActive(false);
    }
}
