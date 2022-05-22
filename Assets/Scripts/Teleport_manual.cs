using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_manual : MonoBehaviour
{

    public GameObject player;
    public Transform wayPoint;
    public GameObject king;

    public int jumps = 0;
    public bool mainPortal = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.position = wayPoint.position + new Vector3(0.5f,0f,0f);
            jumps++;
            if (jumps >= 1 && mainPortal)
            {
                king.SetActive(true);
            }
        }

    }
}
