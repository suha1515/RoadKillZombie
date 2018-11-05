using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalZombie : MonoBehaviour {

    [SerializeField]
    private float hp = 0;
    [SerializeField]
    private float weight = 0;
    [SerializeField]
    private float speed = 0;

    public Transform target;


    /*****************private function*****************/
    private void ZombieMoving()
    {
        if(transform.position.x<target.position.x)
        {
            transform.Translate(Vector2.right * speed);
        }
        else
            transform.Translate(Vector2.left * speed);
    }
    private void TakeDamage(float damage)
    {
        hp -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("bullet"))
        {
            TakeDamage(other.GetComponent<BulletScript>().damage);
        }
    }

    /*****************punblic function*****************/
    //initializer
    public void initZombie(float hp, float weight, float speed, Transform target)
    {
        this.hp = hp;
        this.weight = weight;
        this.speed = speed;
        this.target = target;
    }
    void Start()
    {

    }
    void Update()
    {

    }

}
