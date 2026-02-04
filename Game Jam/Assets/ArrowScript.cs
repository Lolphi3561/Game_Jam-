using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public int arrowSpeed = 50;
    private Transform crossbow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crossbow = GameObject.FindGameObjectWithTag("Crossbow").transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position += crossbow.right * arrowSpeed * Time.deltaTime;
    }
}
