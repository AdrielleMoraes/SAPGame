using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King_behaviour : MonoBehaviour
{
    public GameObject enemy;
    public Animator characterAnimator;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, 8.0f);
    }

    void Spawn()
    {
        // run animation
        // animate attack
        characterAnimator.Play("Attack");
        Instantiate(enemy, this.transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
