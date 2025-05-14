using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLookAt : MonoBehaviour
{
    [SerializeField] Transform manualTarget;
    private Transform target;
    public float rotationSpeed = 5f;

    private void Start()
    {
        if (manualTarget != null)
        {
            target = manualTarget;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("MainCamera").transform;

        }

    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
