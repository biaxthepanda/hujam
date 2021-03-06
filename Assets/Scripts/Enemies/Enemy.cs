using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float damage;
    public float speed;
    public float movementSmoothing;
    public float armor = 1;
    public float range;
    public float innerRange = 3f;
    public bool canFollow;
    public bool isDead;
    public float dieAfterSeconds;
    public GameObject target;
    public GameObject gold;
    public Animator animator;

    Vector2 velocity = Vector2.zero;
    // Start is called before the first frame update

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected virtual void Attack()
    {

    }
    public void GetDamage(int amount)
    {
        health -= amount / armor;
        if(health <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        isDead = true; 
        animator.SetTrigger("Die");
        GetComponent<Collider2D>().enabled = false;
        Debug.LogError("died");
        int goldAmount = (int)Random.Range(1, 6);
        for (int i = 0; i < goldAmount; i++)
        {
            GameObject inst = Instantiate(gold,new Vector2(transform.position.x,transform.position.y+1f),Quaternion.identity);
            inst.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0.3f,0.5f)));
        }
        Invoke("DestroyEnemy",dieAfterSeconds);
    }
    public void Follow()
    {
        if (canFollow)
        {
            if(Vector2.Distance(transform.position,target.transform.position) < range )
            {
                Debug.Log("ucuyor");
                transform.position = Vector2.SmoothDamp(transform.position, target.transform.position, ref velocity, movementSmoothing * Time.deltaTime);
            }
            
        }
    }

    public void DestroyEnemy()
    {
        
        Debug.Log("OLDUUUUU");
        Destroy(gameObject);
    }
}
