using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_controller : MonoBehaviour
{
    private float horizontal;
    public float initial_speed;
    private float speed; 
    private float jumping_power; 
    public float initial_jumpingPower;
    public bool isFacingRight = true;
    public Sprite main; 
    public Sprite head; 
    public int jumpAmount = 1; 
    private int jumpRemaining = 1; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Vector3 initialScale; 
    private bool squished = false; 
    private SpriteRenderer spriteRenderer; 
    private CapsuleCollider2D bodyCollider; 
    private CircleCollider2D headCollider;  
    public GameObject projectilePrefab; 
    public Transform projectileOffset; 
    private Vector3 lastCheckPoint;
    public int ammo; 
    public bool canMove; 
    void Start(){ 
        initialScale = transform.localScale; 
        speed = initial_speed;
        jumping_power = initial_jumpingPower;
        spriteRenderer = GetComponent<SpriteRenderer>();
        headCollider = GetComponent<CircleCollider2D>(); 
        bodyCollider = GetComponent<CapsuleCollider2D>(); 
    }
    private void FixedUpdate()
    {
        if(squished){
            rb.rotation -= rb.velocity.x; 
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    void Update()
    {
        if(squished == false){
            horizontal = Input.GetAxisRaw("Horizontal");
        }else if (IsGrounded()){
            horizontal = Input.GetAxisRaw("Horizontal");
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
        
        if (Input.GetButtonDown("Jump") && jumpRemaining > 0)
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


    void Attack(){
        // Check if player is 'crouched' and stop the ability to attack when they are

        if(!squished){
            if(isFacingRight){
                Instantiate(projectilePrefab, projectileOffset.position, transform.rotation); 
            }else{
                Instantiate(projectilePrefab, projectileOffset.position, new Quaternion(transform.rotation.x,transform.rotation.y,-90,transform.rotation.w));
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
    void SetBodyCollider()
    {
        bodyCollider.enabled = true; 
        headCollider.enabled = false; 
    }
    void SetHeadCollider()
    {
        bodyCollider.enabled = false; 
        headCollider.enabled = true; 
    }


private void OnCollisionEnter2D(Collision2D collision) {
    if(collision.gameObject.CompareTag("Checkpoint")){
        lastCheckPoint = transform.position;
    }
    if(collision.gameObject.CompareTag("Death")){
        transform.position = lastCheckPoint;
    }
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
}
