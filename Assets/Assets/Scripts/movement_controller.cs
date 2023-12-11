using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class movement_controller : MonoBehaviour
{
    [Header("Next Level")]
    public String nextLevel; 
    private Vector3 initialScale; 
    private bool squished = false; 
    private SpriteRenderer spriteRenderer; 
    [Header("Sprite Rendering")]
    public Sprite main; 
    public Sprite head; 
    private Vector3 lastCheckPoint;


    [Header("Colliders")]
    private CapsuleCollider2D bodyCollider; 
    private CircleCollider2D headCollider;
    private CircleCollider2D bounceHead; 

    [Header("Projectile Variables")]
    public GameObject projectilePrefab; 
    public Transform projectileOffset; 
    public float reloadTime; 
    public int ammo; 


    [Header("Movment Variables")]
    public bool canMove; 
    public bool bounce; 
    public int jumpAmount = 1; 
    private int jumpRemaining = 1; 
    public float initial_speed;
    private float speed; 
    private float jumping_power; 
    public float initial_jumpingPower;
    public bool isFacingRight = true;
    private float horizontal;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;



    [Header("Iframe vars")]
    public Color flashCol; 
    public Color returnCol; 
    public float flashDuration; 
    public int flashNumber; 
    public bool canTakeDamage = true; 
    public int health= 3; 
    void Start(){ 
        initialScale = transform.localScale; 
        speed = initial_speed;
        jumping_power = initial_jumpingPower;
        var colliders = GetComponents<CircleCollider2D>(); 
        headCollider = colliders[0]; 
        bounceHead = colliders[1]; 
        spriteRenderer = GetComponent<SpriteRenderer>();
        bounceHead.enabled = false; 
        bodyCollider = GetComponent<CapsuleCollider2D>();  
    } 
    private void FixedUpdate()
    {
        if(squished || bounce){
            rb.rotation -= rb.velocity.x; 
        }
        //Stops head from getting stuck in the wall while in bounce mode
        if(!bounce){rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);}
    }
    void Update()
    {
        
        if (health < 1){ 
            canMove = false; 
        }
        if(canMove){
            if(!bounce){ 
                if(squished == false){
                    horizontal = Input.GetAxisRaw("Horizontal");
                }else if (IsGrounded()){
                    horizontal = Input.GetAxisRaw("Horizontal");
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) && bounce == false){
                SquishBounce(); 
            }
            if(Input.GetKeyUp(KeyCode.LeftControl) && bounce == true){
                UnSquishBounce(); 
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && squished == false){
                Squish(); 
            }
            if(Input.GetKeyUp(KeyCode.LeftShift) && squished == true){
                UnSquish(); 
            }
            
            if  (Input.GetKeyDown(KeyCode.E)){
                Attack(); 
            }
            if (Input.GetButtonDown("Jump") && jumpRemaining > 0 && !bounce)
            {
                jumpRemaining--; 
                rb.velocity = new Vector2(rb.velocity.x, jumping_power);
            }
            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            if (IsGrounded()){
                jumpRemaining = jumpAmount; 
            }
            Flip();
        }
        
    }


    void Attack(){
        // Check if player is 'crouched' and stop the ability to attack when they are
        
        if(!squished && !bounce){
            if(ammo > 0){
                if(isFacingRight){
                    Instantiate(projectilePrefab, projectileOffset.position, transform.rotation); 
                    StartCoroutine(reload()); 
                }else{
                    Instantiate(projectilePrefab, projectileOffset.position, new Quaternion(transform.rotation.x,transform.rotation.y,-90,transform.rotation.w));
                    StartCoroutine(reload()); 
                }
                ammo --; 
            }
            
        } 
        
    }


    //Manage crouching states
    void Squish(){
        speed = initial_speed*0.66f; 
        jumping_power = 0; 
        squished = true; 
        rb.mass = .5f;  
        rb.gravityScale = .5f; 
        //Vector3 newScale = new Vector3(initialScale.x,initialScale.y * 0.5f,initialScale.z);
        spriteRenderer.sprite = head; 
        SetHeadCollider(); 
        //transform.localScale = newScale;
    }
    void UnSquish(){ 
        squished = false ;
        spriteRenderer.sprite = main; 
        //transform.localScale = initialScale; 
        speed = initial_speed; 
        jumping_power = initial_jumpingPower; 
        rb.rotation = 0; 
        rb.mass = 1f;  
         rb.gravityScale = 1f; 
        SetBodyCollider(); 
    }
    void SquishBounce(){
        bounce = true; 
        jumping_power = 0; 
        rb.mass = .5f;  
        rb.gravityScale = .5f; 
        //Vector3 newScale = new Vector3(initialScale.x,initialScale.y * 0.5f,initialScale.z);
        spriteRenderer.sprite = head; 
        SetBounceCollider(); 
        //transform.localScale = newScale;
    }
    void UnSquishBounce(){ 
        bounce = false ;
        spriteRenderer.sprite = main; 
        //transform.localScale = initialScale; 
        speed = initial_speed; 
        jumping_power = initial_jumpingPower; 
        rb.rotation = 0; 
        rb.mass = 1f;  
        rb.gravityScale = 1f; 
        SetBodyCollider(); 
    }
    void SetBodyCollider()
    {
        bodyCollider.enabled = true; 
        headCollider.enabled = false; 
        bounceHead.enabled = false; 
    }
    void SetHeadCollider()
    {
        bodyCollider.enabled = false; 
        headCollider.enabled = true; 
        bounceHead.enabled = false; 
    }
    void SetBounceCollider(){ 
        bodyCollider.enabled = false; 
        headCollider.enabled = false; 
        bounceHead.enabled = true;
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Checkpoint")){
            lastCheckPoint = transform.position;
        }
        if(collision.gameObject.CompareTag("Death")){
            if(canTakeDamage){
                Debug.Log("Hit spike can take damage set to" + canTakeDamage); 
                transform.position = lastCheckPoint;
                Damage(); 
            }
        }
        if(collision.gameObject.CompareTag("Enemy")){
            Damage(); 
        }
        if(collision.gameObject.CompareTag("Finish")){
            NextLevel(); 
        }
    }
    // Iframe code adapted from https://www.youtube.com/watch?v=phZRfEAuW7Q 
    private IEnumerator iFrames(){ 
        int temp = 0; 
        canTakeDamage = false;
        while(temp < flashNumber){
            spriteRenderer.color = flashCol; 
            yield return new WaitForSeconds(flashDuration); 
            spriteRenderer.color = returnCol; 
            yield return new WaitForSeconds(flashDuration);
            temp++; 
        }
        canTakeDamage = true; 
    }
    private void Damage(){ 
        Debug.Log("Entering damage"); 
        if(canTakeDamage){
            Debug.Log("Taking Damage"); 
            health--; 
            if(health < 1){
                Die(); 
            }
            StartCoroutine(iFrames());
        }
    }
    private IEnumerator reload(){ 
        yield return new WaitForSeconds(reloadTime); 
        ammo++; 
    }
    private void Die(){ 
        canMove = false; 
        ammo = 0; 
    }
//Check if playing is touching the ground
    private bool IsGrounded()
    {
        if(squished){
            return Physics2D.OverlapCircle(transform.position,0.4f, groundLayer); 
        }else{
            return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }
        
    }
// Flip player model to be facing the right way
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void NextLevel(){ 
        SceneManager.LoadScene(nextLevel);
    }
}
