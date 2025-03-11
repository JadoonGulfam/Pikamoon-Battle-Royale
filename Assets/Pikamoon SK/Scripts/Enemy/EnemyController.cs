using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyController : MonoBehaviour,IDamageable
{
    [SerializeField] float Health;
    [SerializeField] Animator anim;

    [SerializeField] Transform HealthUI;
    
    [SerializeField] Image HealthBar;

    [SerializeField] Camera Cam;

    public void OnDamage()
    {
        OnDamage(1);
    }

    public void OnDamage(float damageAmount)
    {
    }

    public void OnDamage(float damageAmount, Transform hitPoint)
    {
        Vector2 dir = GetHitDirection(hitPoint);

        anim.SetFloat("XVal",dir.x);
        anim.SetFloat("YVal",dir.y);

        anim.SetTrigger("GetHit");

        Health -= damageAmount;

        HealthBar.DOFillAmount(Health/100, .1f);

        if(isKilled())
            this.gameObject.SetActive(false);

    }

    public bool isKilled()
    {
        return Health < 0;
    }

    Vector2 GetHitDirection(Transform hit)
    {
        // Get the direction the enemy is facing
        Vector3 enemyForward = transform.forward;

        // Get the direction from the enemy to the attacker
        Vector3 directionToAttacker = (hit.position - transform.position).normalized;

        // Calculate the dot product between the enemy's forward direction and the direction to the attacker
        float dotProduct = Vector3.Dot(enemyForward, directionToAttacker);

        // Calculate the cross product to determine if the hit came from the left or right
        Vector3 crossProduct = Vector3.Cross(enemyForward, directionToAttacker);

        // Initialize XVal and YVal
        float XVal = 0f;
        float YVal = 1f;

        // Determine if the hit came from the front, back, left, or right and set XVal/YVal accordingly
        if (dotProduct > 0.5f)
        {
            // Hit from the front
            YVal = 1f;
            XVal = 0f;
        }
        else if (dotProduct < -0.5f)
        {
            // Hit from the back
            YVal = -1f;
            XVal = 0f;
        }
        else
        {
            // Hit from the sides
            if (crossProduct.y > 0)
            {
                // Hit from the right
                XVal = 1f;
                YVal = 0f;
            }
            else
            {
                // Hit from the left
                XVal = -1f;
                YVal = 0f;
            }
        }

        return new Vector2(XVal,YVal);

    }

    void MakeRotationOfUITowardsCam()
    {

        // Get the direction to the camera but ignore the Y-axis
        Vector3 directionToCamera = Cam.transform.position - transform.position;

        // Flatten the direction vector to the Y-axis only
        directionToCamera.y = 0;

        // If the direction vector is non-zero, rotate the health bar towards the camera
        if (directionToCamera != Vector3.zero)
        {
            // Calculate the rotation towards the camera on the Y-axis
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

            // Apply the rotation to the health bar
            transform.rotation = targetRotation;
        }

    }
}
