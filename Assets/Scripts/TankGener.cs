using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGener : MonoBehaviour
{
    [NonSerialized]
    public bool isEnemy = false;
    public GameObject tank;
    public float time = 5.0f;

    public void Start()
    {
        StartCoroutine(SpawnTank());
    }

    IEnumerator SpawnTank()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(time);
            Vector3 pos = new Vector3(
                transform.GetChild(0).position.x + UnityEngine.Random.Range(4f, 8f),
                transform.GetChild(0).position.y,
                transform.GetChild(0).position.z + UnityEngine.Random.Range(4f, 8f)
                );
            GameObject spawn = Instantiate(tank, pos, Quaternion.identity);

            if (isEnemy)
                spawn.tag = "Enemy";
        }
    }

}
