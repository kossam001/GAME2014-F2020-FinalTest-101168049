/* ShrinkingPlatformController.cs
 * 
 * Name: Samuel Ko
 * ID: 101168049
 * Last Modified: 2020-12-16
 * 
 * A shrinking platform where if the player stands on it, it shrinks, otherwise it grows back to original size.
 * 
 * 2020-12-16: Added this script.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatformController : MonoBehaviour
{
    public float maxWidth;
    public float maxSizeChangeDuration = 2;
    public bool isShrinking = false;

    private float width;
    public float timer = 0;

    private void Start()
    {
        maxWidth = width = transform.localScale.x;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShrinking = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShrinking = false;
        }
    }

    private void Update()
    {
        if (width < maxWidth || isShrinking)
        {
            ChangeSize(0.0f, maxWidth);
        }
        else
        {
            timer = 0;
        }
    }

    void ChangeSize(float from, float to)
    {
        width = Mathf.Lerp(maxWidth, 0.0f, timer / maxSizeChangeDuration);

        if (isShrinking)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (width < 0)
        {
            width = 0;
            isShrinking = false;
        }
        else if (width > maxWidth)
        {
            width = maxWidth;
        }

        transform.localScale = new Vector3(width, transform.localScale.y);
    }
}
