using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float distance;
    public LayerMask solidelements;
    //public float lifetime;

    // Start is called before the first frame update
    void Start()
    {
             Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {

       /* RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.up, distance, solidelements);
        if(hitinfo.collider != null)
        {
            if(hitinfo.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Took damage");
            }
            DestroyProjectile();
        }*/
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CompareTag("Enemy"))
        {
            Debug.Log("Enemy Took damage");
        }
        DestroyProjectile();
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    
}
