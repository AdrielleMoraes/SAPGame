using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; // ai to controll enemies 


public static class EnemiesKeys
{
    public const string ANIMATION_MOVEMENT_KEY = "Movement";
    public const string ANIMATION_ATTACK_KEY = "Attack";
}
public class Enemies_Controller : MonoBehaviour
{

    public Transform target;
    public GameObject floatingTxt;

    public GameController gameManager;

    // Combat Settings
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDistance = 3f;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float attackDamage = 10;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Transform attackPoint;
    public LayerMask playerLayer;

    private float canAttack=1;
    private float currentHealth;

    public Transform enemyGFX;
    Path path;
    int currentWayPoint = 0;

    bool reachedTarget = false;

    // Reference to the Animator
    public Animator characterAnimator;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        // keep updating path
        InvokeRepeating("UpdatePath", 0f, 0.5f);

        currentHealth = maxHealth;
        
    }

    // Chasing main character
    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete); // last parameter is a function that will run after path is completed
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0; // reset waypoint
            reachedTarget = true;
        }
    }

    // Combat system
    public void UpdateHealth(float damage)
    {
        currentHealth += damage;
        if (currentHealth <= 0)
        {
            gameManager.enemyCount--;
            Die();
        }
    }

    void Die()
    {
        this.enabled = false;
        Destroy(this.gameObject, 2f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                other.GetComponent<Player_controller>().UpdateHealth(-attackDamage);

                // create a floating text
                GameObject floatingText = Instantiate(floatingTxt, other.transform.position, Quaternion.identity);
                floatingText.GetComponent<InGameText>().PointAmount = attackDamage.ToString();

                // animate attack
                characterAnimator.Play(EnemiesKeys.ANIMATION_ATTACK_KEY);
                canAttack = 0;
            }
            else
            {
                canAttack += Time.deltaTime;
            }

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        // Detect players in range of attack       
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        // check if there's a valid path
        if (path == null || hitPlayers.Length == 0)
        {
            return;
        }
        else
        {
            // check if path is complete
            if (currentWayPoint >= path.vectorPath.Count)
            {
                reachedTarget = true;
                return;
            }
            else
            {
                reachedTarget = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

            // animate walk
            characterAnimator.SetFloat(Keys.ANIMATION_MOVEMENT_KEY, Mathf.Abs(direction.x + direction.y));

            //move on to next waypoint
            if (distance < nextWaypointDistance)
            {
                currentWayPoint++;
            }

            // correct sprite direction
            if (force.x >= 0.1f)
            {
                //enemyGFX.localScale = new Vector3(1f, 1f, 1f);
                enemyGFX.transform.transform.eulerAngles = new Vector3(
                        enemyGFX.transform.eulerAngles.x,
                        0,
                        enemyGFX.transform.eulerAngles.z);
            }
            else if (force.x < -0.1f)
            {
                //enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
                enemyGFX.transform.transform.eulerAngles = new Vector3(
                enemyGFX.transform.eulerAngles.x,
                180,
                enemyGFX.transform.eulerAngles.z);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
