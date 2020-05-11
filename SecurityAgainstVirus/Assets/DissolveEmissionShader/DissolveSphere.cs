using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveSphere : MonoBehaviour 
{
    public float value;

    private Material mat;
    private float startTime;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        value = Mathf.Sin(Time.time) / 2 + 0.5f;
        startTime = Time.time;
    }

    private void Update()
    {
        if (value < 0.99f)
        {
            mat.SetFloat("_DissolveAmount", value);
            value = Mathf.Sin(Time.time - startTime) / 2 + 0.5f;
        }
    }
}