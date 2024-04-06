using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContl : MonoBehaviour
{
    [NonSerialized] public Vector3 position;
    public float speed = 30f;
    public int damage = 20;

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
        
        if(transform.position == position)
            Destroy(gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            TankAttack attack = other.GetComponent<TankAttack>();
            attack._hp -= damage;

            Transform hpBar = other.transform.GetChild(1).transform;
            hpBar.localScale = new Vector3(
                hpBar.localScale.x - 0.6f,
                hpBar.localScale.y,
                hpBar.localScale.z
                );

            if(attack._hp < 0)
                Destroy(other.gameObject);
        }
    }

}
