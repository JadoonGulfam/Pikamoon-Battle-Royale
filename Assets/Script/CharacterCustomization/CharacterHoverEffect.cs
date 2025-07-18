using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CharacterHoverEffect : MonoBehaviour
{  
    public Vector3 originalScale;
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f); // Scale increase factor
    public float hoverSpeed = 0.3f; // Speed of scaling
    public static bool isSelected = false; // Flag to check if character is selected
    public Vector3 selectedPosition = Vector3.zero; // The target position for the selected character
    public GenderType gendertype;
    void Start()
    {
        originalScale = transform.localScale;
        isSelected = false;
    }
    //void OnMouseEnter()
    //{
    //    if (!isSelected) // Only run hover effect if not selected
    //    {
    //        StopAllCoroutines();
    //      StartCoroutine(ScaleOverTime(hoverScale));
    //    }
    //}

    //void OnMouseExit()
    //{
    //    if (!isSelected) // Only run hover effect if not selected
    //    {
    //        StopAllCoroutines();
    //        StartCoroutine(ScaleOverTime(originalScale));
    //    }
    //}

    IEnumerator ScaleOverTime(Vector3 targetScale)
    {
        float elapsed = 0f;
        Vector3 startScale = transform.localScale;

        while (elapsed < hoverSpeed)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / hoverSpeed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
    void Update()
    {
        // Detect mouse click and raycast to check if the GameObject was clicked
        if (!isSelected && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float raycastDistance = 100f; // Raycast distance

            // Visualize the raycast in the Scene view
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 1f);

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.transform == transform)
                {
                    OnSelectCharacter();
                }
            }
        }
    }
    void OnSelectCharacter()
    {
        if (!isSelected) // Proceed only if not already selected
        {
            isSelected = true; // Mark as selected to disable further hover effect           
            GameManager.instance._player = this.gameObject;
            //// DisableCollider all other characters except the selected one
            //foreach (GameObject character in GameManager.instance.instantiatedPlayers)
            //{
            //    if (character != this.gameObject)
            //    {
            //        //Debug.Log("char "+ character.name);
            //        character.SetActive(false);
            //    }
            //}
            // Move the selected character to the target position
            StartCoroutine(MoveToPosition(selectedPosition));
            // Optionally, you can add any code here to finalize selection, like deactivating this script
            // or triggering an animation on the selected character.

            this.enabled = false; // DisableCollider this script to prevent further selection
            GameManager.instance.characterdata.gender = gendertype.ToString();

          //  GameManager.instance.uiManager.startBtn.interactable = true;
        }
    }
    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float moveSpeed = 5f;  // Adjust speed as necessary
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition; // Ensure exact final position
    }
}
