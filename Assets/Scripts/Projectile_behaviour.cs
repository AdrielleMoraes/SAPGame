using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_behaviour : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private float attackDamage = 10f;

    public GameObject floatingTxt;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player_controller>().UpdateHealth(-attackDamage);

            // create a floating text
            GameObject floatingText = Instantiate(floatingTxt, other.transform.position, Quaternion.identity);
            floatingText.GetComponent<InGameText>().PointAmount = attackDamage.ToString();

        // self destroy after dealing damage
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
