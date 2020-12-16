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
    public float duration = 2;
    public float growthMultiplier = 1;
    public float shrinkMultiplier = 1;
    public bool isShrinking = false;

    private float maxWidth;
    private float width;
    private float timer = 0;

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
            if (isShrinking)
            {
                ChangeSize(shrinkMultiplier);
            }
            else
            {
                ChangeSize(growthMultiplier);
            }
        }
        else
        {
            timer = 0;
        }
    }

    void ChangeSize(float multiplier)
    {
        width = Mathf.Lerp(maxWidth, 0.0f, timer / duration);

        if (isShrinking)
        {
            timer += Time.deltaTime * multiplier;
        }
        else
        {
            timer -= Time.deltaTime * multiplier;
        }

        if (width <= 0)
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
