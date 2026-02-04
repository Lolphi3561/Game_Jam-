using System;
using UnityEngine;

public class deadlyArrowScript : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.root != transform.root)
        {
            Destroy(transform.root.gameObject);
        }
        else
        {
            Debug.Log("aaaa");
        }
    }
}
