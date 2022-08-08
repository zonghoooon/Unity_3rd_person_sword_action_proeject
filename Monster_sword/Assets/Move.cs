using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector2 movement = new Vector2();
    Vector2 rotate = new Vector2();
    private float speed = 5f;
    public float rspeed = 5f;
    public float fspeed = 1f;
    private float dist;
    Animator animator;
    private bool isGround = true;

    private Rigidbody rb;
    public GameObject Body;
    public GameObject Sword;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody>();

    }
    private void Update()
    {
        UpdateState();
        Planecheck();
    }
    void FixedUpdate()
    {
        move();
        Jump();
        Attack();

    }

    private void UpdateState()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (Input.GetKey("left shift"))
        {
            animator.SetBool("Sprint", true);
            speed = 8f;
        }
        else
        {
            animator.SetBool("Sprint", false);
            speed = 5f;
        }

    }
    private void Planecheck()
    {
        Ray ray = new Ray();
        ray.origin = Body.transform.position;
        ray.direction = Vector3.down;
        RaycastHit Hit;
        if (Physics.Raycast(ray, out Hit) == true)
        {
            if (Hit.collider.tag == "floor")
            {
                if (Hit.distance < 0.2f)
                {
                    isGround = true;
                }
                else
                {
                    isGround = false;
                }
            }

        }

    }


    private void move()
    {
        Vector3 dir = transform.localRotation * Vector3.forward;
        Vector3 dir2 = transform.localRotation * Vector3.right;
        movement.x = 0;
        movement.y = 0;
        rotate.x = 0;
        movement.x = Input.GetAxis("Horizontal") * speed;
        movement.y = Input.GetAxis("Vertical") * speed;
        rotate.x = Input.GetAxis("Mouse X") * rspeed;
        rotate.x = Mathf.Clamp(rotate.x, -80, 80);

        transform.position += (dir * movement.y) * Time.deltaTime;
        transform.position += (dir2 * movement.x) * Time.deltaTime;

        transform.eulerAngles += new Vector3(0, rotate.x, 0);

    }
    

    private void Jump()
    {

        if (Input.GetKeyDown("space"))
        {
            if (isGround)
            {
                rb.AddForce(Vector3.up * 3000f, ForceMode.Impulse);
                animator.SetBool("Jump", true);
                isGround = false;
            }
        }
        if (isGround)
        {
            animator.SetBool("Jump", false);
        }
    }
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetInteger("Attack", 1);
            
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
   animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1f)
        {
            Sword.GetComponent<Sword>().changer(true);

        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
   animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            animator.SetInteger("Attack", 0);
            Sword.GetComponent<Sword>().changer(false);

        }
    }
}
