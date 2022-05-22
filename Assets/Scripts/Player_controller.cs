using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



/// <summary>
/// Animator keys
/// </summary>
public static class Keys
{
    public const string ANIMATION_MOVEMENT_KEY = "Movement";
    public const string ANIMATION_SWORD_KEY = "Sword";
    public const string ANIMATION_RANGED_KEY = "Ranged";
}


[RequireComponent(typeof(Rigidbody2D))]
public class Player_controller : MonoBehaviour
{
    [Header("Movement")]
    public float movingSpeed;

    private Rigidbody2D rigidbodyplayer;
    float moveDirection = 1f;
    private float maxHealth = 100;
    public float currentHealth;

    public GameController gameManager;

    Vector2 movement;

    // Reference to the Animator
    public Animator characterAnimator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackDamage = 10f;
    public LayerMask enemiesLayers;
    private float canAttack = 1;
    private float attackSpeed = 1;
    public Text txt;
    public GameObject floatingTxt;
    private void Awake()
    {
        // Get references
        rigidbodyplayer = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        movement.y = Input.GetAxisRaw("Vertical");

        characterAnimator.SetFloat(Keys.ANIMATION_MOVEMENT_KEY, Mathf.Abs(movement.x + movement.y));

        if (Input.GetMouseButtonDown(0))
        {
            if (attackSpeed <= canAttack)
            {
                Attack();
            }

        }
        canAttack += Time.deltaTime;
    }

    private void Attack()
    {
        canAttack = 0;
        // Play Attack Animation
        characterAnimator.Play(Keys.ANIMATION_SWORD_KEY);
        // Detect Enemies in range of attack
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position,attackRange, enemiesLayers);
        // Apply damage to enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemies_Controller>().UpdateHealth(-attackDamage);
            Debug.Log(enemy.name);

            // create a floating text
            GameObject floatingText = Instantiate(floatingTxt, enemy.transform.position, Quaternion.identity);
            floatingText.GetComponent<InGameText>().PointAmount = attackDamage.ToString();
        }
    }

    private void FixedUpdate()
    {
        movement.Normalize();
        rigidbodyplayer.MovePosition(rigidbodyplayer.position + movement * movingSpeed * Time.deltaTime);

        // Calculate move speed and direction
        if (!Mathf.Approximately(movement.x, 0))
        {
            moveDirection = Mathf.Sign(movement.x);
        }
        transform.localScale = new Vector3(moveDirection, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NextLevel")
        {
            //AudioSource.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // combat functions
    public void UpdateHealth(float damage)
    {
        currentHealth += damage;
        txt.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            Die();
        }

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    void Die()
    {
        gameManager.GameOver();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
