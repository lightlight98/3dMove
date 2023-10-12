using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;

    bool JDown;
    bool isJump;

    Vector3 moveVec;

    Animator animator;
    Rigidbody _rigidbody;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Move();
        GetInput();
        Turn();
        Jump();
    }

    void Move() 
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;

        animator.SetBool("isRun", moveVec != Vector3.zero);

        
    }
    void GetInput() 
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        JDown = Input.GetButtonDown("Jump");
    }
    void Turn() 
    {
        transform.LookAt(transform.position + moveVec);
    }
    void Jump() 
    {
        if (JDown && !isJump) 
        {
            _rigidbody.AddForce(Vector3.up * 20, ForceMode.Impulse);
            animator.SetBool("isJump", true);
            animator.SetTrigger("doJump");
            isJump = true;
        }
        

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") 
        {
            animator.SetBool("isJump", false);
            isJump = false;
        }
    }
}
