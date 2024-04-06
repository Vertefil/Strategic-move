using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TankAttack : MonoBehaviour
{
    [NonSerialized] public int _hp = 100;

    public float radius = 100f;
    public GameObject bullet;
    private Coroutine _coroutine = null;

    private void Update()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
        Collider[] hitBoxs = Physics.OverlapSphere(transform.position, radius);
        if (hitBoxs.Length == 0 && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;

            // if players destroyed
            if (gameObject.CompareTag("Enemy"))
                GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
        }

        foreach (var hits in hitBoxs)
        {
            if ((gameObject.CompareTag("Player") && hits.gameObject.CompareTag("Enemy")) ||
                 (gameObject.CompareTag("Enemy") && hits.gameObject.CompareTag("Player")))
            {
                if (gameObject.CompareTag("Enemy"))
                    GetComponent<NavMeshAgent>().SetDestination(hits.transform.position);

                if (_coroutine == null)
                    _coroutine = StartCoroutine(StartAttack(hits));



            }
        }
    }

    IEnumerator StartAttack(Collider enemyPos)
    {
        GameObject bul = Instantiate(bullet, transform.GetChild(2).position, Quaternion.identity);
        bul.GetComponent<BulletContl>().position = enemyPos.transform.position;
        yield return new WaitForSeconds(2f);
        
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
