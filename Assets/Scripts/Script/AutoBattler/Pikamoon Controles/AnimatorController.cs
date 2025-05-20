using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    int attack1Hash;
    int attack2Hash;
    int isDieHash;
    int moveHash;
    int takeDamage1;
    int takeDamage2;
    int victory;


    private float value = 0f;         // Initial value
    private float targetValue = 1f;   // Target value
    private float duration = 5f;      // Duration over which to increase the value
    private float elapsedTime = 0f;

    bool targetLocked = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        attack1Hash = Animator.StringToHash("Attack1");
        attack2Hash = Animator.StringToHash("Attack2");
        isDieHash = Animator.StringToHash("IsDie");
        moveHash = Animator.StringToHash("MoveVelocity");
        takeDamage1 = Animator.StringToHash("TakeDamage1");
        takeDamage2 = Animator.StringToHash("TakeDamage2");
        victory = Animator.StringToHash("IsVictory");


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack(1, true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Attack(1, false);
            value = 0;
            elapsedTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Attack(2, true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Attack(2, false);
            value = 0;
            elapsedTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool(isDieHash,true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            TakeDamage(2);
        }
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            Victory();
        }








        if(!targetLocked)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the interpolation factor (t)
            float t = elapsedTime / duration;

            // Smoothly transition the value from 0 to targetValue
            value = Mathf.Lerp(0f, targetValue, t);

            // Ensure the value doesn't exceed targetValue
            value = Mathf.Clamp(value, 0f, targetValue);
            animator.SetFloat(moveHash, value);
        }

      

    }
    public void TargetLocked()
    {
        targetLocked = true;
        animator.SetFloat(moveHash, 0);
        value = 0;
        elapsedTime = 0;
    }

    public void Attack(int i, bool attackStatues)
    {
        TargetLocked();
        if (i==1)
        {
            animator.SetBool(attack1Hash, attackStatues);
        }
        else
        {
            animator.SetBool(attack2Hash, attackStatues);
        }

    }
    public void TakeDamage(int i)
    {
        if (i == 1)
        {
            animator.SetTrigger(takeDamage1);
        }
        else
        {
            animator.SetTrigger(takeDamage2);
        }

    }

    public void Victory()
    {
        animator.SetTrigger(victory);
    }

}
