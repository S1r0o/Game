using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    //public GameObject bomb;
    private float timer = 5F;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            anim.Play("Expl");
        }
        else
            anim.Play("Bomb_On");
    }

    public void DestroyBomb()
    {
        Destroy(gameObject);
    }
}
