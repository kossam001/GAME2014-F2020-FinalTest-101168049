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
    private float timer = 0;

    private void Start()
    {
        maxWidth = width = transform.localScale.x;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Shrink();
        }
    }

    void Shrink()
    {
        if (width >= 0)
        {
            width = Mathf.Lerp(maxWidth, 0.0f, timer / maxSizeChangeDuration);
            timer += Time.deltaTime;
        }
        else
        {
            width = 0;
        }

        transform.localScale = new Vector3(width, transform.localScale.y);
    }
}
