using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParticales : MonoBehaviour
{
    public float speed = 5f; // Speed of the movement along the Z-axis

    public bool isAiCast;
    private void OnEnable()
    {
        StartCoroutine(MoveForTwoSeconds());
    }
    //private void Start()
    //{
    //    // Start the movement coroutine
    //    StartCoroutine(MoveForTwoSeconds());
    //}
    
    private IEnumerator MoveForTwoSeconds()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 2f)
        {
            // Move the particle along the Z-axis
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }
        gameObject.SetActive(false);
        // After 2 seconds, stop the particle or destroy it
       // Destroy(gameObject); // Optional: Destroy the particle after movement
    }
}
