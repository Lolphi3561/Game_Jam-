using TMPro;
using UnityEngine;

public class chainsawScript : MonoBehaviour
{
    private Vector3 startPoint;
    public Vector3 endPoint;
    private Vector3 targetPoint;

    public float chainsawSpeed = 0.2f;

    public float chainsawRotationSpeed = -5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPoint = transform.position;
        targetPoint = startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -chainsawRotationSpeed));

        transform.position = Vector3.MoveTowards(transform.position, targetPoint, chainsawSpeed*Time.deltaTime);
        if(transform.position == targetPoint)
        {
            if (targetPoint == startPoint)
            {
                targetPoint = endPoint;
                chainsawRotationSpeed *= -1;
            }
            else
            {
                targetPoint = startPoint;
                chainsawRotationSpeed *= -1;
            }
        }
    }
}
