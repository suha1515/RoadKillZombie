using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{

    public float speedDownValue;
    public float speedUpvalue;

    public UILabel speedLabel;
    public GameObject carLight;

    public BgScrolling bs;
    public AudioSource engineSound;

    enum pos
    {
        FRONT = 0, SIDE,
        REAR
    };
    [SerializeField]
    public Transform[] bus_pos = new Transform[3];
    [SerializeField]
    public Sprite[] zombie_sp = new Sprite[3];
    //정면=front,옆면=side,후면=rear
    public UIManager um;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CarLight();
        SpeedDown();
    }
    void FixedUpdate()
    {

    }
    private void CarLight()
    {
        if (LKZ_GameManager.Instance.isNight)
        {
            carLight.SetActive(true);
        }
        else
        {
            carLight.SetActive(false);
        }
    }
    private void SpeedDown()
    {
        if (LKZ_GameManager.Instance.stick_zombie.Count > 0)
        {
            if (LKZ_GameManager.Instance.bus_speed > 0.0f)
            {
                LKZ_GameManager.Instance.bus_speed -= speedDownValue * LKZ_GameManager.Instance.stick_zombie.Count * Time.deltaTime;
                speedLabel.text = LKZ_GameManager.Instance.bus_speed.ToString("N0");
            }
            else
            {
                speedLabel.text = "0";
                um.GameOver();
            }
        }
        else if (LKZ_GameManager.Instance.stick_zombie.Count == 0)
        {
            if (LKZ_GameManager.Instance.bus_speed < 80f)
            {
                LKZ_GameManager.Instance.bus_speed += speedUpvalue * Time.deltaTime;
                speedLabel.text = LKZ_GameManager.Instance.bus_speed.ToString("N0");
            }
        }
        if (bs != null)
        {
            bs.scrollSpeed = Converter.Remap(LKZ_GameManager.Instance.bus_speed, 0, 80, 0, 3);
        }
        else
        {
            Debug.Log("bus에 bs Scroll 스크립트 넣어야 함");
        }
        engineSound.volume = Converter.Remap(LKZ_GameManager.Instance.bus_speed, 0, 80, 0, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            // 좀비 달라붙는 소리 on
            collision.transform.parent.GetComponent<LKZ_ZombieWalk>().no_loop.clip = collision.transform.parent.GetComponent<LKZ_ZombieWalk>().ride_clip;
            collision.transform.parent.GetComponent<LKZ_ZombieWalk>().no_loop.volume = 1;
            collision.transform.parent.GetComponent<LKZ_ZombieWalk>().no_loop.Play();
            int Randpos = Random.Range(0, 3);
            LKZ_GameManager.Instance.stick_zombie.Add(collision.transform.parent.gameObject);
            collision.transform.parent.GetComponent<LKZ_ZombieWalk>().state = ZOMBIE_STATE.HANG;
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = zombie_sp[Randpos];
            collision.transform.parent.position = new Vector2(bus_pos[Randpos].position.x + Random.Range(-1.0f, 1.0f),
                                                        bus_pos[Randpos].position.y + Random.Range(-1.0f, 1.0f));
        }
    }

}