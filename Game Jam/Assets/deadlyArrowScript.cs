using System;
using UnityEngine;

public class deadlyArrowScript : MonoBehaviour
{

    public Transform platform = null;
    public Rigidbody2D platformRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = new Vector2(transform.position.x, platformRb.position.y);
        platformRb.MovePosition(targetPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.root != transform.root)
        {
            Destroy(transform.root.gameObject);
        }
    }
}
