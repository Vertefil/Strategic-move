using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    public Transform[] points;
    public GameObject hangar;

    private void Start()
    {
        StartCoroutine(SpawnHangars());
    }
    
    IEnumerator SpawnHangars()
    {
        for(int i = 0;  i < points.Length; i++)
        {
            yield return new WaitForSeconds(10f);
            GameObject spawn = Instantiate(hangar);
            Destroy(spawn.GetComponent<PlaceObject>());
            spawn.transform.position = points[i].position;
            spawn.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
            spawn.GetComponent<TankGener>().enabled = true;
            spawn.GetComponent<TankGener>().isEnemy = true;
        }
    }
}
