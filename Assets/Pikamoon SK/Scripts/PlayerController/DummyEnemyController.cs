using UnityEngine;
using UnityEngine.UI;

namespace Pikamoon.Controller
{

    public class DummyEnemyController : MonoBehaviour,IDamageable
    {
        AnimationController AC;
        SFXController SFX;
        [SerializeField] AudioClip damage;

        public bool IsRootMotionEnabled;

        public float health;
        public float JumpVelocity;
        [SerializeField] bool AllowLookPlayer;
        [SerializeField] Transform DummyAttacker;
        [SerializeField] bool AllowDummyInputHit;
        CharacterController characterController;

        [Header("UI")]
        public Image healthFiller;
        public Image ShieldFiller;

        public void Start()
        {

            AC = GetComponent<AnimationController>();
            characterController = GetComponent<CharacterController>();
            SFX = GetComponent<SFXController>();
        }

        void Update()
        {
            if (AllowDummyInputHit && Input.GetKeyDown(KeyCode.C))
            {
                GetHit(DummyAttacker);
            }
            if(AllowLookPlayer)
                LookAtPlayer();
        }

        void LookAtPlayer()
        {
            Vector3 Mid = (this.transform.position - DummyAttacker.position).normalized;

            Mid.y = this.transform.position.y;  

            this.transform.LookAt(DummyAttacker,Vector3.up);
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

        void GetHit(Transform hit)
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
                XVal = 0f;
                YVal = 1f;
            }
            else if (dotProduct < -0.5f)
            {
                // Hit from the back
                XVal = 0f; 
                YVal = -1f;
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

            AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, XVal);
            AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, YVal);
            AC.PAnimator.CrossFadeInFixedTime(AC.Parameters.GetHit.Hash, 0.1f);

            SFX.PlayDamageSound(damage);

            //if (dotProduct > 0.5f)
            //{
            //    // Hit from the front

            //    AC.PAnimator.CrossFadeInFixedTime("Hit.Front", 0.1f);
            //}
            //else if (dotProduct < -0.5f)
            //{
            //    // Hit from the back

            //    AC.PAnimator.CrossFadeInFixedTime("Hit.Back", 0.1f);
            //}
            //else
            //{
            //    // Hit from the sides
            //    if (crossProduct.y > 0)
            //    {
            //        // Hit from the right

            //        AC.PAnimator.CrossFadeInFixedTime("Hit.Right", 0.1f);
            //    }
            //    else
            //    {
            //        // Hit from the left

            //        AC.PAnimator.CrossFadeInFixedTime("Hit.Left", 0.1f);
            //    }
            //}
        }
        public void RootMove(Vector3 direction)
        {
            characterController.Move(direction);
        }

        void Gravity()
        {
            
        }

        //void MakeRotationOfUITowardsCam()
        //{

        //    // Get the direction to the camera but ignore the Y-axis
        //    Vector3 directionToCamera = Cam.transform.position - transform.position;

        //    // Flatten the direction vector to the Y-axis only
        //    directionToCamera.y = 0;

        //    // If the direction vector is non-zero, rotate the health bar towards the camera
        //    if (directionToCamera != Vector3.zero)
        //    {
        //        // Calculate the rotation towards the camera on the Y-axis
        //        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        //        // Apply the rotation to the health bar
        //        transform.rotation = targetRotation;
        //    }

        //}


        #region IDamageable Properties

        public float Health
        {
            get { return health; }
            private set { health = value; }
        }

        public bool isKilled()
        {
            if(health <= 0)
                return true;

            return false;
        }

        public void OnDamage()
        {
        }

        public void OnDamage(float damageAmount)
        {
        }

        public void OnDamage(float damageAmount, Transform hitPoint)
        {
            GetHit(DummyAttacker);


            health -= damageAmount;
            //HealthBar.DOFillAmount(health / 100, .1f);

            //if (isKilled())
            //    this.gameObject.SetActive(false);

        }

        #endregion
    }

}