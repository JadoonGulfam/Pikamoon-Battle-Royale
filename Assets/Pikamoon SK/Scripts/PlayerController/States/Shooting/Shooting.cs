using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;



namespace Pikamoon.Controller
{
    [System.Serializable]
    public class Range
    {
        public float min;
        public float max;
    }
    public class Shooting : State
    {
        RangedWeapon ActiveWeapon;

        public Transform FirePoint;
        public Transform ArrowHoldingPoint;
        
        [Space]
        [Header("Animation Rigging")]

        [SerializeField] Rig AR_LockedOnTargetAimer;
        [SerializeField] Transform AR_LookTarget;
        [SerializeField] float AR_LookSpeed;
        
        [Space]
        [Header("Aim")]

        [SerializeField] LayerMask AimableMask;
        [SerializeField] float MaxAimRange;
        [Space]
        [SerializeField] bool LookTowardCameraForward;
        [SerializeField] float RotOffsetDuringAim;
        [Space]
        [SerializeField] float CancelAimAfterSeconds;
        [SerializeField] float CancelAttackAfterSeconds;

        [Space]
        [SerializeField] Transform DebugTransform;
        [SerializeField] RectTransform DebugUITransform;

        bool _isAiming;
        bool _isInAttack;
        bool AllowFire;
        float riggingVal;
        float FireRate;
        Vector2 screenCenterPoint;
        Coroutine cancelAimRoutine;


        public PlayerSetupForMultiplayer MP_Setup;


        [Header("Charged Attack")]
        [Space]
        [SerializeField] bool isChargingAttack;
        GameObject ChargeGO;
        GameObject ChargeFillerGO;
        [SerializeField] AnimationCurve ChargedScalingCurve;
        [Range(1f,3f)][SerializeField] float chargeAttackDamageMultiplier = 1;
        [SerializeField] float SpeedOfCharge = 100f;
        float currentChargeValue;
        [SerializeField] bool isShotPerfect;
        [HideInInspector] Range PerfectRange = new Range { min = 0.7f, max = 0.9f };

        [ContextMenu("Get Holding Pos")]
        public void GetHoldingPos()
        {
            ArrowHoldingPoint = Controller.holdingPoints[1].Point;
        }

        public override void Initialize()
        {
            base.Initialize();

            ChargeGO = Controller.UI.hudcontroller.ChargeGO;
            ChargeFillerGO = Controller.UI.hudcontroller.ChargeFillerGO;

            // playerInput.onAttack1_Up += StartChargedAttack;
            playerInput.onAttack1_Down += PlayFireAnimation;
            playerInput.onAttack2_Down += StartAim;
            playerInput.onAttack2_Up += CancelAim;

            screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            AllowFire = true;
        }
        public override void Initialize(Transform Root)
        {
            base.Initialize(Root);

            ChargeGO = Controller.UI.hudcontroller.ChargeGO;
            ChargeFillerGO = Controller.UI.hudcontroller.ChargeFillerGO;

            //playerInput.onAttack1_Up += StartChargedAttack;
            playerInput.onAttack1_Down += PlayFireAnimation;
            playerInput.onAttack2_Down += StartAim;
            playerInput.onAttack2_Up += CancelAim;


            screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            AllowFire = true;
        }
        public override StateType GetStateType()
        {
            return StateType.Shooting;
        }
        private void Update()
        {
            //if (Controller.MP_Setup != null && !Controller.MP_Setup.isMinePlayer)
            //    return;

            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;


            if (Controller.ActiveWeapon.Data.Type != WeaponType.Ranged)
            {
                ActiveWeapon = null;
                return;
            }

            AimRigging();

            if(isChargingAttack)
                ManageChargedAttack();

            if (!Controller.IsInAttack)
                return;

            HandleAnimation();
            MoveDuringAim();
            RotatePlayerTowardsCamFor();
        }

        void StartChargedAttack()
        {
            ResetChargedAttackTimer();
            isShotPerfect = false;
            isChargingAttack = true; 
            if (!ChargeGO.activeInHierarchy)
                ChargeGO.gameObject.SetActive(true);
        }
        
        void ManageChargedAttack()
        {
            currentChargeValue += Time.deltaTime * SpeedOfCharge / 100;
            currentChargeValue = Mathf.Clamp01(currentChargeValue);

            
            if (ChargeGO.activeInHierarchy)
                ChargeFillerGO.transform.localScale = Vector3.one * ChargedScalingCurve.Evaluate(currentChargeValue);

            if (currentChargeValue >= 1)
            {
                if (ChargeGO.activeInHierarchy)
                {
                    if (ChargeGO.activeInHierarchy)
                        ChargeGO.gameObject.SetActive(false);

                }
            }

        }

        void CancelChargedAttack() 
        {
            isChargingAttack = false; 

            if (ChargeGO.activeInHierarchy)
                ChargeGO.gameObject.SetActive(false);
        }

        void ResetChargedAttackTimer()
        {
            currentChargeValue = 0;
            ChargeFillerGO.transform.localScale = Vector3.zero;

            if (!ChargeGO.activeInHierarchy)
                ChargeGO.gameObject.SetActive(true);
        }

        //public override void Initialize()
        //{
        //    if (Controller.ActiveWeapon.Data.weaponType != WeaponType.Ranged)
        //        return;

        //    AssignWeapon();
        //}

        void AssignWeapon()
        {
            if(Controller.ActiveWeapon.Prefab is RangedWeapon)
            {
                ActiveWeapon = Controller.ActiveWeapon.Prefab as RangedWeapon;

                FireRate = ActiveWeapon.GetFireRate();
            }
        }


        public void ActivateWeapon(Weapon _weapon)
        {

            ActiveWeapon = Controller.ActiveWeapon.Prefab as RangedWeapon;

            FireRate = ActiveWeapon.GetFireRate();
        }

        void AimRigging()
        {
            if (_isAiming)
            {
                if(riggingVal < 1)
                    riggingVal += Time.deltaTime * AR_LookSpeed;


                AR_LookTarget.position = FirePoint.position;
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

                Ray ray = Controller.cameraController._camera.ScreenPointToRay(screenCenterPoint);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 999f, AimableMask))
                {
                    AR_LookTarget.position = hit.point;
                }
            }
            else
            {
                if (riggingVal > 0)
                    riggingVal -= Time.deltaTime * AR_LookSpeed;
            }


            AR_LockedOnTargetAimer.weight = riggingVal;
        }

        void HandleSpeed()
        {
            if(playerInput.isMoving)
            {
                Controller.ChangeSpeed(Controller.PlayerData.WalkSpeed,0.2f);
            }
            else
            {
                Controller.ChangeSpeed(0,0);
            }
        }

        void HandleAnimation()
        {
            AC.PAnimator.SetFloat(AC.Parameters.Speed.Hash, Controller.AnimSpeed);
            if (Controller.IsGrounded && !Controller.IsInAttack)
            {
                AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, playerInput.isMoving);
            }
        }

        void MoveDuringAim()
        {

            HandleSpeed();

            Vector3 direction = Controller.GetDirectionAccordingToCameraWhenMoving();


            AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, playerInput.Horizontal);
            AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, playerInput.Vertical);


            // Always apply vertical velocity (for gravity or jumping)
            Vector3 finalMove = new Vector3(direction.x * Controller.Speed, playerInput.JumpVelocity, direction.z * Controller.Speed);

            // Move the character based on calculated velocity and speed
            Controller.Move(finalMove);
        }

        void RotatePlayerTowardsCamFor()
        {
            if(LookTowardCameraForward)
            {
                Controller.RotatePlayerTowardsCameraForwardDirectionDuringAim(10,RotOffsetDuringAim);
            }
        }

        void StartAim()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Ranged || Controller.IsSwimming)
                return;

            StartChargedAttack();

            Controller.cameraController.ChangeCam(Cam.Aim);
            Controller.cameraController.ChangeAimZoom(true);

            AC.PAnimator.SetLayerWeight(1, 1);

            Controller.IsInAttack = true;
            _isAiming = true;
            LookTowardCameraForward = true;

            AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, true);
            AC.PAnimator.SetBool(AC.Parameters.isAiming.Hash, true);

            isChargingAttack = true;

            ActiveWeapon.Pull();
        }
        void CancelAim()
        {
            if (Controller.ActiveWeapon.Data.Type != WeaponType.Ranged)
                return;

            CancelChargedAttack();

            ReferencesHolder.Instance._cameraController.ChangeCam(Cam.Default);
            ReferencesHolder.Instance._cameraController.ChangeAimZoom(false);

            _isAiming = false;

            AC.PAnimator.SetBool(AC.Parameters.isAiming.Hash, false);
            AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, 0);

            Controller.IsInAttack = false;

            isChargingAttack = false;

            LookTowardCameraForward = false;

            ActiveWeapon.Release();

        }

        IEnumerator CancelAimAndAttack()
        {
            float waitTime = _isInAttack ? CancelAimAfterSeconds : 0f;

            yield return new WaitForSeconds(waitTime);

            LookTowardCameraForward = false;

            if (!_isAiming)
            {
                ActiveWeapon.Release();
                ReferencesHolder.Instance._cameraController.ChangeAimZoom(false);
                ReferencesHolder.Instance._cameraController.ChangeCam(Cam.Default);
            }
            //AR_LockedOnTargetAimer.weight = 0;
            AC.PAnimator.SetBool(AC.Parameters.isAiming.Hash, false);
            Controller.IsInAttack = false;
            _isInAttack = false;

            yield return new WaitForSeconds (CancelAttackAfterSeconds);

            ActiveWeapon.Release();
            AC.PAnimator.SetLayerWeight(1, 0);
            AC.PAnimator.SetBool(AC.Parameters.inCombat.Hash, false);
        }
        
        
        public void PlayFireAnimation()
        {
            if (!AllowFire || Controller.ActiveWeapon.Data.Type != WeaponType.Ranged || Controller.IsSwimming)
                return;

            _isInAttack = true;

            Controller.IsInAttack = true;
            
            if (!_isAiming)
                ReferencesHolder.Instance._cameraController.ChangeCam(Cam.Aim);

            AC.PAnimator.SetLayerWeight(1, 1);

            if (isChargingAttack)
                if (currentChargeValue >= PerfectRange.min && currentChargeValue <= PerfectRange.max)
                    isShotPerfect = true;

            ResetChargedAttackTimer();

            ActiveWeapon.Pull();
            AC.PAnimator.SetBool(AC.Parameters.isAiming.Hash, true);
            AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, true);
            AC.PAnimator.SetTrigger(AC.Parameters.Shoot.Hash);

            LookTowardCameraForward = true;

            StartCoroutine(RegulateFire());

            if (cancelAimRoutine != null)
                StopCoroutine(cancelAimRoutine);

            if(!_isAiming)
                cancelAimRoutine = StartCoroutine(CancelAimAndAttack());
        }

        public void ShootArrow()
        {
            Ray ray = Controller.cameraController._camera.ScreenPointToRay(screenCenterPoint);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999f, AimableMask))
            {
                if(ActiveWeapon == null)
                {
                    AssignWeapon();
                }

                if (ActiveWeapon)
                {
                    ActiveWeapon.ShootBullet(hit.point, isShotPerfect ? chargeAttackDamageMultiplier:1);

                    //Controller.cameraController.EnableBulletActionCam(ActiveWeapon.GetActionCamParent());
                    DebugTransform.transform.position = hit.point;
                }
            }
        }
        public void PickArrow()
        {
            if (ActiveWeapon == null)
            {
                AssignWeapon();
            }

            if(ActiveWeapon)
                ActiveWeapon.EnableActiveArrow(ArrowHoldingPoint);
        }
        public void PutBackArrow()
        {
            if (ActiveWeapon == null)
            {
                AssignWeapon();
            }

            if (ActiveWeapon)
                ActiveWeapon.DisableActiveArrow();
        }

        void EndAttack()
        {

        }

        IEnumerator RegulateFire()
        {
            AllowFire = false;

            yield return new WaitForSeconds(FireRate);
            AllowFire = true;
        }


        private void OnDestroy()
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

            //playerInput.onAttack1_Up -= StartChargedAttack;
            playerInput.onAttack1_Clicked -= PlayFireAnimation;
            playerInput.onAttack2_Down -= StartAim;
            playerInput.onAttack2_Up -= CancelAim;
        }

        public override void OnEnd()
        {
        }

        public override void OnStart()
        {
        }

        public override void OnUpdate()
        {
        }

    }

#if UNITY_EDITOR
    // This ensures the editor code is only compiled in the Unity Editor
    [CustomEditor(typeof(Shooting))]
    public class ShootingEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw the default Inspector first
            DrawDefaultInspector();

            Shooting script = (Shooting)target;

            // Draw a min-max slider
            EditorGUILayout.MinMaxSlider(
                new GUIContent("Perfect Range Slider"),
                ref script.PerfectRange.min,
                ref script.PerfectRange.max,
                0f, 1f);

            // Optional numeric fields below
            EditorGUILayout.BeginHorizontal();
            script.PerfectRange.min = EditorGUILayout.FloatField("Min", script.PerfectRange.min);
            script.PerfectRange.max = EditorGUILayout.FloatField("Max", script.PerfectRange.max);
            EditorGUILayout.EndHorizontal();

            // Clamp to 0–1 range
            script.PerfectRange.min = Mathf.Clamp01(script.PerfectRange.min);
            script.PerfectRange.max = Mathf.Clamp01(script.PerfectRange.max);

            if (GUI.changed)
                EditorUtility.SetDirty(script);
        }
    }
#endif

}