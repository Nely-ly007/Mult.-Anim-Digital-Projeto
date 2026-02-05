using UnityEngine;
using DragonBones;
public class PlayerController : MonoBehaviour 
{ 
    private UnityArmatureComponent armature; 
    private Rigidbody2D rb; 
    private bool isGrounded = true; void Start() { armature = GetComponent<UnityArmatureComponent>(); 
        rb = GetComponent<Rigidbody2D>(); } void Update() { float moveInput = Input.GetAxis("Horizontal"); 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) { rb.linearVelocity = new Vector2(rb.linearVelocity.x, 10f);
        armature.animation.Play("pula", 1); isGrounded = false; } 
        else if (Mathf.Abs(moveInput) > 0.1f && isGrounded) { armature.animation.Play("anda", 0); } 
        else if (isGrounded) { armature.animation.Play("idle", 0); } 
        if (moveInput > 0) armature.armature.flipX = false;
            else if (moveInput < 0) armature.armature.flipX = true; }
        void OnCollisionEnter2D(Collision2D collision)
        { 
            if (collision.gameObject.CompareTag("Ground")) isGrounded = true; 
        } 
}