using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject projectile;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0, attackSpeed);

    }

    void Shoot()
    {
        transform.right = player.position - transform.position;
        Instantiate(projectile, FirePoint.position, FirePoint.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
