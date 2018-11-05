using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKZ_PlayerSprite : MonoBehaviour
{

    [SerializeField] List<Sprite> sprites;
    LKZ_Player lkz_Player;
    SpriteRenderer sr;
    float timer = 0.0f;
    // Use this for initialization
    void Start()
    {
        lkz_Player = GetComponent<LKZ_Player>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

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

    void Idle()
    {
        if (timer > 0.5f)
        {
            sr.flipX = true;
            if (sr.sprite == sprites[0])
            {
                sr.sprite = sprites[1];
            }
            else
            {
                sr.sprite = sprites[0];
            }
            timer = 0.0f;
        }
    }
    void Left()
    {
        sr.sprite = sprites[2];
        sr.flipX = true;
    }
    void Right()
    {
        sr.sprite = sprites[3];
        sr.flipX = false;
    }


}
