using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private bool isFadeOut = false;
    private float speed = 0.5f;
    private Vector3 targetScale;
    // Update is called once per frame

    private void Start()
    {
        targetScale = this.transform.localScale * 2;
    }
    void Update()
    {
        if (isFadeOut)
        {
            float t = 1 / speed * Time.deltaTime;
            this.transform.localScale = Vector3.Lerp(transform.localScale, targetScale, t);

            Color c = transform.GetComponent<MeshRenderer>().material.color;
            c.a = Mathf.Lerp(c.a, 0, t);
            transform.GetComponent<MeshRenderer>().material.color = c;
        }
    }

    internal void FadeOut()
    {
        isFadeOut = true;
    }
}
