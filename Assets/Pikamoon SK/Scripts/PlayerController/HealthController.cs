using Pikamoon.UI;
using UnityEngine;
using Fusion;

namespace Pikamoon.Controller
{
    public class HealthController : NetworkBehaviour
    {
        PlayerController Controller;
        AnimationController AC;
        InventoryController inventoryController;

        // inspector defaults (kept so you can set starting values in the editor)
        [SerializeField] float inspectorHeadShieldValue = 0f;
        [SerializeField] float inspectorUpperShieldValue = 0f;
        [SerializeField] float inspectorLowerShieldValue = 0f;
        [Space]
        [SerializeField] float inspectorHealth = 100f;

        // networked state (names kept similar to your original properties)
        [Networked] 
        public float headShieldValue { get; set; }

        [Networked] 
        public float upperShieldValue { get; set; }

        [Networked] 
        public float lowerShieldValue { get; set; }

        [Networked] 
        public float health { get; set; }

        private ChangeDetector _changeDetector;

        public void Awake()
        {
            AC = GetComponent<AnimationController>();
            Controller = GetComponent<PlayerController>();
            inventoryController = GetComponent<InventoryController>();
        }

        public override void Spawned()
        {
            _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);

            // initialize networked values from inspector defaults on StateAuthority only
            if (Object.HasStateAuthority)
            {
                headShieldValue = inspectorHeadShieldValue;
                upperShieldValue = inspectorUpperShieldValue;
                lowerShieldValue = inspectorLowerShieldValue;
                health = inspectorHealth;
            }

            // ensure UI is in correct state on spawn
            UpdateUI();
        }

        public void Initialize()
        {
            if (Controller == null)
            {
                Debug.Log("Controller is Null");
            }
            if (Controller != null && Controller.UI == null)
            {
                Debug.Log("Controller ui is Null");
            }

            if (Controller != null && Controller.UI != null && Controller.UI.hudcontroller == null)
            {
                Debug.Log("Controller ui hud controller is Null");
            }

            UpdateUI();
        }

        public override void Render()
        {
            foreach (var change in _changeDetector.DetectChanges(this))
            {
                if (change == nameof(health) ||
                    change == nameof(headShieldValue) ||
                    change == nameof(upperShieldValue) ||
                    change == nameof(lowerShieldValue))
                {
                    UpdateUI();
                }
            }
        }

        Vector2 GetHitDirection(Transform hit)
        {
            Vector3 enemyForward = transform.forward;
            Vector3 directionToAttacker = (hit.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(enemyForward, directionToAttacker);
            Vector3 crossProduct = Vector3.Cross(enemyForward, directionToAttacker);

            float XVal = 0f;
            float YVal = 1f;

            if (dotProduct > 0.5f)
            {
                YVal = 1f;
                XVal = 0f;
            }
            else if (dotProduct < -0.5f)
            {
                YVal = -1f;
                XVal = 0f;
            }
            else
            {
                if (crossProduct.y > 0)
                {
                    XVal = 1f;
                    YVal = 0f;
                }
                else
                {
                    XVal = -1f;
                    YVal = 0f;
                }
            }

            return new Vector2(XVal, YVal);
        }

        void UpdateUI()
        {
            if (Controller == null || Controller.UI == null || Controller.UI.hudcontroller == null) return;

            Controller.UI.hudcontroller.UpdateHealth(health, 100);
            Controller.UI.hudcontroller.UpdateHeadShield(headShieldValue, 100);
            Controller.UI.hudcontroller.UpdateUpperShield(upperShieldValue, 100);
            Controller.UI.hudcontroller.UpdateLowerShield(lowerShieldValue, 100);
        }



        // attackers should call this to request damage; it sends RPC to the victim's authority
        public void TakeDamage(HealthPointType healthPoint, float damageAmount, Transform hitPoint)
        {
            print("11111111 called take damage health point" + healthPoint + "     damage amount  " + damageAmount + "    hit point" + hitPoint);
            // call RPC on victim's StateAuthority to apply damage (RpcTargets.StateAuthority)
            RPC_RequestDamage(healthPoint, damageAmount, Object.Id, hitPoint.position);
        }

        // RPC runs on victim's StateAuthority only and then updates all clients
        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void RPC_RequestDamage(HealthPointType healthPoint, float damageAmount, NetworkId attackerId, Vector3 hitPos, RpcInfo info = default)
        {
            ApplyDamageOnAuthority(healthPoint, damageAmount, hitPos);
        }

        // centralised damage application; runs on authority (or locally in offline mode)
        void ApplyDamageOnAuthority(HealthPointType healthPoint, float damageAmount, Vector3 hitPos)
        {
            if (Runner != null && !Object.HasStateAuthority) return; // only authority applies (except offline)

            Vector2 dir;
            // try to compute direction using a Transform if possible — fallback if hitPos is zero
            if (hitPos != Vector3.zero)
            {
                var dummy = new GameObject("tmpHit").transform;
                dummy.position = hitPos;
                dir = GetHitDirection(dummy);
                Destroy(dummy.gameObject);
            }
            else
            {
                dir = new Vector2(0f, 1f);
            }

            if (AC != null)
            {
                AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, dir.x);
                AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, dir.y);
                AC.PAnimator.SetTrigger(AC.Parameters.GetHit.Hash);
            }

            float remainingDamage = damageAmount;

            switch (healthPoint)
            {
                case HealthPointType.Head:
                    if (inventoryController != null && inventoryController.Shields.items[0] != null)
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
                    if (inventoryController != null && inventoryController.Shields.items[1] != null)
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
                    if (inventoryController != null && inventoryController.Shields.items[2] != null)
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

            if (isKilled())
            {
                Controller.inventory.PlaceLootBoxAfterDeath();
                gameObject.SetActive(false);
            }
        }

        public void OnDamage(float damageAmount, Transform hitPoint)
        {
            if (Runner != null && !Object.HasStateAuthority)
            {
                // request authority to apply simple damage
                RPC_RequestDamage(HealthPointType.UpperBody, damageAmount, Object.Id, hitPoint.position);
                return;
            }

            Vector2 dir = GetHitDirection(hitPoint);

            AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, dir.x);
            AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, dir.y);

            AC.PAnimator.SetTrigger(AC.Parameters.GetHit.Hash);

            health -= damageAmount;

            if (isKilled())
                gameObject.SetActive(false);
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

      
        #endregion
    }
}
