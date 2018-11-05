using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZOMBIE_STATE { DEAD = 0, SPHONE, WALK, HANG }
public enum ZOMBIE_KIND { NORMAL = 0, BIG, BOSS }
public class LKZ_ZombieWalk : MonoBehaviour
{
    public AudioClip die_ride_Clip;
    public AudioClip die_shoot_Clip;
    public AudioClip ride_clip;
    public AudioSource no_loop;

    public ZOMBIE_KIND ZombieKind;
    public ZOMBIE_STATE state;
    Animator animator;

    private bool isDie =false;
    private float timer = 0.0f;
    [Range(0.05f, 0.2f)] public float fWalkSpeed = 0.1f;
    private float fSpriteSpeed = 0.0f;
    [Range(0.1f, 3.0f)] public float fReadyTime = 1.0f;
    public int Heath;
    public List<Sprite> sprites;
    int spriteNum = 0;
    [SerializeField] private float fDeadSpeed = 0.0f;
    public SpriteRenderer spriteRenderer;
    public EffectManager em;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        state = ZOMBIE_STATE.SPHONE;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Heath <= 0)
        {
            state = ZOMBIE_STATE.DEAD;
        }

        switch (state)
        {
            case ZOMBIE_STATE.SPHONE:
                if (Timer(fReadyTime))
                {
                    state = ZOMBIE_STATE.WALK;
                    isDie = false;
                }
                break;
            case ZOMBIE_STATE.WALK:
                ZombieWalk();
                break;
            case ZOMBIE_STATE.HANG:
                animator.SetTrigger("Ride");
                break;
            case ZOMBIE_STATE.DEAD:
                  animator.SetTrigger("Die");
                if (Timer(fDeadSpeed))
                {
                    for (int i = 0; i < LKZ_GameManager.Instance.stick_zombie.Count; i++)
                    {
                        if (gameObject.name == LKZ_GameManager.Instance.stick_zombie[i].name)
                        {
                            LKZ_GameManager.Instance.stick_zombie.RemoveAt(i);
                        }
                    }
                    RoundManager.remainZombie--;
                    gameObject.SetActive(false);
                    spriteRenderer.color = new Color(1, 1, 1, 1);
                    spriteRenderer.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 100);
                    spriteRenderer.transform.localPosition = Vector3.zero;
                }
                else
                {
                    // 죽는 소리 on
                    if (isDie == false)
                    {
                        isDie = true;

                        no_loop.clip = die_shoot_Clip;
                        no_loop.volume = 0.5f;
                        no_loop.Play();
                    }
                }

                break;
        }
    }

    public void Init()
    {
        switch (ZombieKind)
        {
            case ZOMBIE_KIND.NORMAL:
                Heath = 1;
                break;

            case ZOMBIE_KIND.BIG:
                Heath = 2;
                break;

            case ZOMBIE_KIND.BOSS:
                Heath = 4;
                break;
        }
        fSpriteSpeed = 0.05f / fWalkSpeed;
        timer = 0.0f;
        spriteNum = 0;

    }
    bool Timer(float _maxTime)
    {
        bool result = false;

        timer += Time.deltaTime;
        if (timer > _maxTime)
        {
            timer = 0.0f;
            result = true;
        }
        return result;
    }


    void ZombieWalk()
    {
        SpriteChange();
        transform.Translate(Vector3.left * fWalkSpeed);
    }

    void SpriteChange()
    {
        if (Timer(fSpriteSpeed))
        {
            ++spriteNum;
            if (spriteNum >= sprites.Count)
            {
                spriteNum = 0;
            }
            spriteRenderer.sprite = sprites[spriteNum];

        }
    }
}
