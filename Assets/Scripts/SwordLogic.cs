using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLogic : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float throwPower;
    [SerializeField] private float damageDone;
    [SerializeField] private Rigidbody rb;

    private void Update()
    {
        rb.AddForce(transform.up * throwPower, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            collision.transform.GetComponent<Rigidbody>();
            Die();
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
