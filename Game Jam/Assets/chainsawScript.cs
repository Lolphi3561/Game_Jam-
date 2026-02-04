using UnityEngine;

public class chainsawScript : MonoBehaviour
{
    private Vector3 startPoint;
    public Vector3 endPoint;
    private Vector3 targetPoint;

    public float chainsawSpeed = 0.2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPoint = transform.position;
        targetPoint = startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, chainsawSpeed*Time.deltaTime);
        if(transform.position == targetPoint)
        {
            if (targetPoint == startPoint)
            {
                targetPoint = endPoint;
            }
            else
            {
                targetPoint = startPoint;
            }
        }
    }
}
