using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDot : MonoBehaviour
{
    public Transform cameraTransform;
    public float size = 0.005f;

    private void OnEnable()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform; 
    }

    private void Update()
    {
        float scale = Vector3.Distance(transform.position, cameraTransform.position);
        transform.localScale =Vector3.one * scale * size;
    }
}
