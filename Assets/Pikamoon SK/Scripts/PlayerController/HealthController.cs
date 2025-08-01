using Pikamoon.UI;
using UnityEngine;

namespace Pikamoon.Controller
{

    public class HealthController : MonoBehaviour
    {
        PlayerController Controller;
        AnimationController AC;
        InventoryController inventoryController;

        [SerializeField] float headShieldValue;
        [SerializeField] float upperShieldValue;
        [SerializeField] float lowerShieldValue;
        [Space]
        [SerializeField] float health;



        public void Start()
        {
            AC = GetComponent<AnimationController>();
            Controller = GetComponent<PlayerController>();
            inventoryController = GetComponent<InventoryController>();
        }
        public void Initialize()
        {
            Controller.UI.hudcontroller.UpdateHealth(health, 100);

            Controller.UI.hudcontroller.UpdateHeadShield(headShieldValue, 100);
            Controller.UI.hudcontroller.UpdateUpperShield(upperShieldValue, 100);
            Controller.UI.hudcontroller.UpdateLowerShield(lowerShieldValue, 100);
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

            return new Vector2(XVal, YVal);

        }


        #region IDamageable Properties

        public float Health
        {
            get { return health; }
            private set { health = value; }
        }
        public float HeadShieldValue
        {
            get { return headShieldValue; }
            private set { headShieldValue = value; }
        }
        public float UpperShieldValue
        {
            get { return upperShieldValue; }
            private set { upperShieldValue = value; }
        }
        public float LowerShieldValue
        {
            get { return lowerShieldValue; }
            private set { lowerShieldValue = value; }
        }

        public bool isKilled()
        {
            if (health <= 0)
                return true;

            return false;
        }

        public void TakeDamage(HealthPointType healthPoint, float damageAmount, Transform hitPoint)
        {
            Vector2 dir = GetHitDirection(hitPoint);


            AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, dir.x);
            AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, dir.y);

            AC.PAnimator.SetTrigger(AC.Parameters.GetHit.Hash);

            float remainingDamage = damageAmount;

            switch (healthPoint)
            {
                case HealthPointType.Head:
                    if (inventoryController.Shields.items[0] != null)
                    {
                        headShieldValue = inventoryController.Shields.items[0].Quantity;

                        remainingDamage = headShieldValue;

                        headShieldValue = headShieldValue - damageAmount;

                        if (headShieldValue < 0)
                        {
                            headShieldValue = 0;
                            remainingDamage = damageAmount - remainingDamage;
                        }
                        else
                        {
                            remainingDamage = 0;
                        }

                        inventoryController.Shields.items[0].Quantity = headShieldValue;

                        Controller.UI?.hudcontroller.UpdateHeadShield(headShieldValue, 100);
                    }


                    break;
                case HealthPointType.UpperBody:

                    if (inventoryController.Shields.items[1] != null)
                    {
                        upperShieldValue = inventoryController.Shields.items[1].Quantity;

                        remainingDamage = upperShieldValue;

                        upperShieldValue = upperShieldValue - damageAmount;

                        if (upperShieldValue < 0)
                        {
                            upperShieldValue = 0;
                            remainingDamage = damageAmount - remainingDamage;
                        }
                        else
                        {
                            remainingDamage = 0;
                        }

                        inventoryController.Shields.items[1].Quantity = upperShieldValue;

                        Controller.UI?.hudcontroller.UpdateUpperShield(upperShieldValue, 100);
                    }
                    break;
                case HealthPointType.LowerBody:

                    if (inventoryController.Shields.items[2] != null)
                    {
                        lowerShieldValue = inventoryController.Shields.items[2].Quantity;

                        remainingDamage = lowerShieldValue;

                        lowerShieldValue = lowerShieldValue - damageAmount;

                        if (lowerShieldValue < 0)
                        {
                            lowerShieldValue = 0;
                            remainingDamage = damageAmount - remainingDamage;
                        }
                        else
                        {
                            remainingDamage = 0;
                        }

                        inventoryController.Shields.items[2].Quantity = lowerShieldValue;

                        Controller.UI?.hudcontroller.UpdateLowerShield(lowerShieldValue, 100);
                    }
                    break;
            }

            if (remainingDamage > 0)
            {
                health -= remainingDamage;
                if (health < 0)
                {
                    health = 0;
                }
            }

            Controller.UI?.hudcontroller.UpdateHealth(health, 100);

            //HealthBar.DOFillAmount(health / 100, .1f);

            if (isKilled())
            {
                Controller.inventory.PlaceLootBoxAfterDeath();
                gameObject.SetActive(false);
            }
        }


        public void OnDamage(float damageAmount, Transform hitPoint)
        {
            Vector2 dir = GetHitDirection(hitPoint);


            AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, dir.x);
            AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, dir.y);

            AC.PAnimator.SetTrigger(AC.Parameters.GetHit.Hash);

            health -= damageAmount;

            //HealthBar.DOFillAmount(health / 100, .1f);

            if (isKilled())
                gameObject.SetActive(false);

        }
        #endregion
    }

}