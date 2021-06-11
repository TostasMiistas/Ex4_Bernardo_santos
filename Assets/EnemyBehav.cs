using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehav : MonoBehaviour
{
    private Vector3 destination;
    public float distance;
    public float speed;

    private void Awake()
    {
        destination = new Vector3(transform.localPosition.x + distance, transform.localPosition.y);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(Vector3.Distance(transform.localPosition, destination) < .1f)
        {
            destination = new Vector3(-destination.x, transform.localPosition.y);
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, destination, speed * Time.deltaTime);
    }

}
