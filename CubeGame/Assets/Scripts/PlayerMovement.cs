using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager GameManager;
    public float moveSpeed;
    private float maxSpeed = 5f;
    public GameObject deathParticles;

    private Vector3 input;

    private Vector3 spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
        GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position += input * moveSpeed * Time.deltaTime;
        
        if(transform.position.y < -2)
            Die();
        
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            GameManager.CompleteLevel();
        }

        if (other.gameObject.CompareTag("Token"))
        {
            GameManager.AddCoin();
            Destroy(other.gameObject);
        }
    }

    void Die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        transform.position = spawn;
    }
}
