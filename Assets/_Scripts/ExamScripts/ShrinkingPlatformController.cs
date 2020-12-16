/* ShrinkingPlatformController.cs
 * 
 * Name: Samuel Ko
 * ID: 101168049
 * Last Modified: 2020-12-16
 * 
 * A shrinking platform where if the player stands on it, it shrinks, otherwise it grows back to original size.
 * 
 * 2020-12-16: Added this script.
 * 2020-12-16: Platform shrinks by lerping deltatime.
 * 2020-12-16: Figured out that I can reverse the accumulated timer to grow.  Combined shrink and grow.
 * 2020-12-16: Added sections to stop timer so that shrinking and growing happens without too much delay.
 * 2020-12-16: Fixed bug where shrinking to width 0 no longer grows.
 * 2020-12-16: Added growth/shrink speed modifiers.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatformController : MonoBehaviour
{
    // At base, how long to shrink and grow completely
    public float duration = 2;

    // Controls shrink and grow speed
    public float growthMultiplier = 1;
    public float shrinkMultiplier = 1;
    public bool isShrinking = false;

    public AudioClip growSound;
    public AudioClip shrinkSound;

    private float maxWidth;
    private float width;
    private float timer = 0;
    private AudioSource sounds;

    private void Start()
    {
        sounds = GetComponent<AudioSource>();
        maxWidth = width = transform.localScale.x;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShrinking = true;

            // So it plays once
            sounds.clip = shrinkSound;
            sounds.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShrinking = false;

            // So it plays once
            sounds.clip = growSound;
            sounds.Play();
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
            // Timer is set to 0 here so that shrinking happens immediately when player lands on the platform
            timer = 0;
        }
    }

    void ChangeSize(float multiplier)
    {
        // Lerping the width of the platform from max width to 0 using a ratio of deltatime and desired duration
        width = Mathf.Lerp(maxWidth, 0.0f, timer / duration);

        // Since timer is shared, the width that the platform is currently at is remembered
        if (isShrinking)
        {
            // Adding time shrinks
            timer += Time.deltaTime * multiplier;
        }
        else
        {
            // Subtracting time grows
            timer -= Time.deltaTime * multiplier;
        }

        // OnCollisionExit2D may not happen if the collider goes to 0, so turning shrinking off here
        if (width <= 0)
        {
            width = 0; // Round the width to 0
            isShrinking = false;

            // So it plays once
            sounds.clip = growSound;
            sounds.Play();
        }
        else if (width > maxWidth)
        {
            width = maxWidth; // Round the width to maxWidth
        }

        // Set the scale using the calculated width
        transform.localScale = new Vector3(width, transform.localScale.y);
    }
}
