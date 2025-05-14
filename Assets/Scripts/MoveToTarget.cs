using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] bool ignoreY;
    
    [SerializeField] private float distance;
    [SerializeField] float yWall;
    [SerializeField] bool useYWall;
    [SerializeField] float test;

    private void Start()
    {
       
    }

    private void Update()
    {
        if (ignoreY)
        {
            IgnoreY();
        }
        else
        {
            FullPosition();
        }

    }

    void FullPosition()
    {
        if (Vector3.Distance(transform.position, target.position) > 2)
        {
            transform.position = target.position;
            transform.rotation = Quaternion.identity;
        }

        if (Vector3.Distance(transform.position, target.position) < distance) return;

        if (target.position.y <= yWall && useYWall)
        {
            Vector3 wallPosition = new Vector3 (target.position.x, yWall, target.position.z);
            target.position = wallPosition;
        }

        transform.position = Vector3.Lerp(transform.position, target.position, smoothTime * Time.deltaTime);
    }

    void IgnoreY()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);

        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                             new Vector3(targetPosition.x, 0, targetPosition.z)) > 2)
        {
            transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            transform.rotation = Quaternion.identity;
        }

        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                             new Vector3(targetPosition.x, 0, targetPosition.z)) < distance) return;

        Transform targetCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, smoothTime * Time.deltaTime);
        transform.position = new Vector3(newPosition.x, targetCamera.position.y+test, newPosition.z);
    }
}
