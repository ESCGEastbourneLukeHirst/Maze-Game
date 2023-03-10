using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 6f;
    public float rotationspeed = 6f;

    public Rigidbody rb;

    Vector3 movement;

    float horizontal;
    float vertical;
    [SerializeField] private bool canMove;

    private void Start()
    {
        canMove = true;
    }

    public IEnumerator ForceStopMovement(float waitSecs)
    {
        horizontal = 0;
        vertical = 0;
        canMove = false;

        yield return new WaitForSeconds(waitSecs);

        canMove = true;
    }

    public void Update()
    {
        if(!canMove)
            return;

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0, vertical);
        movement.Normalize();

        if(movement != Vector3.zero)
        {
            animator.SetBool("Walking", true);
        }
        else if (movement == Vector3.zero)
        {
            animator.SetBool("Walking", false);
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            Quaternion torotation = Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, torotation, rotationspeed * Time.deltaTime);
        }
    }


}
