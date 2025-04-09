using UnityEngine;
using UnityEngine.Events;

namespace Pikamoon.Controller
{


    public class Capturing : State
    {
        public Transform OriginCapturePosition;
        public Transform TargetCapturePosition;
        [Space]
        public float RangeToCheckForPikamoon;
        [Space]
        public LayerMask CaptureLayerMask;
        [Space]
        public ParticleSystem captureParticle;
        [Space]
        public PlayerSetupForMultiplayer MP_Setup;

        public UnityEvent OnStunnedPikamoonFounded;
        public UnityEvent OnStunnedPikamoonLost;
        public UnityEvent OnCapturedSuccessfully;

        bool hasTarget;

        bool isCapturing, isCaptureComplete;
        float timeToCapture;
        float captureTimer;

        ICapturable _icapturable;
        public override void Initialize()
        {
            base.Initialize();

            playerInput.onCapture_Down += StartCapturing;
            playerInput.onCapture_Up += CancelCapturing;
        }
        public override void Initialize(Transform Root)
        {
            base.Initialize(Root);

            playerInput.onCapture_Down += StartCapturing;
            playerInput.onCapture_Up += CancelCapturing;
        }
        public override StateType GetStateType()
        {
            return StateType.Capture;
        }
        private void Update()
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

            hasTarget = false;

            if(!isCapturing)
            {
                if (Controller == null || Controller.CurrentPlayerState != StateType.Locomtion)
                    return;

                CheckForCapturableTransform();
            }
            else
            {
                OnStunnedPikamoonLost?.Invoke();
                FaceTowardsCapturingTransform();

                captureTimer += Time.deltaTime;
                if (captureTimer >= timeToCapture)
                {
                    SuccessfullyCaptured();
                }
            }


        }

        void CheckForCapturableTransform()
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

            Ray ray = Controller._cameraController._camera.ScreenPointToRay(screenCenterPoint);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, RangeToCheckForPikamoon, CaptureLayerMask))
            {
                _icapturable = hit.transform.GetComponent<ICapturable>();

                if (_icapturable != null)
                {
                    CapturedInfo captureInfo;

                    if (_icapturable.onCapture(out captureInfo))
                    {
                        hasTarget = true;
                        timeToCapture = captureInfo.TimeToCapture;
                        TargetCapturePosition = captureInfo.transform;
                        OnStunnedPikamoonFounded?.Invoke();
                    }
                }
            }
            else
            {
                OnStunnedPikamoonLost?.Invoke();
            }
        }


        public void FaceTowardsCapturingTransform()
        {
            Vector3 direction = (transform.localPosition + TargetCapturePosition.localPosition) / 2;

            //direction.y = transform.position.y;

            direction.y = this.transform.position.y;


            transform.LookAt(direction);
        }

        public void SuccessfullyCaptured()
        {
            isCaptureComplete = true;
            isCapturing = false;


            Controller._cameraController.ToggleCaptureCam(false);

            TargetCapturePosition.gameObject.SetActive(false);

            Controller.CurrentPlayerState = StateType.Locomtion;

            AC.PAnimator.SetInteger(AC.Parameters.SecondaryState.Hash, 52);

            OnCapturedSuccessfully?.Invoke();
        }

        public void StartCapturing()
        {
            if (Controller.CurrentPlayerState != StateType.Locomtion)
                return;

            if(hasTarget)
            {
                captureTimer = 0;

                Controller._cameraController.ToggleCaptureCam(true);

                Controller.CurrentPlayerState = StateType.Capture;

                isCapturing = true;
                isCaptureComplete = false;

                AC.PAnimator.SetInteger(AC.Parameters.SecondaryState.Hash, 51);
                AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, false);
            }
        }
        
        public void CancelCapturing()
        {
            isCapturing = false;

            Controller._cameraController.ToggleCaptureCam(false);

            AC.PAnimator.SetBool   (AC.Parameters.isWalkRun.Hash      , true );
            AC.PAnimator.SetInteger(AC.Parameters.SecondaryState.Hash , 50   );

            Controller.CurrentPlayerState = StateType.Locomtion;
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

        private void OnDestroy()
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;


            playerInput.onCapture_Down  -=  StartCapturing  ;
            playerInput.onCapture_Up    -=  CancelCapturing ;
        }
    }
}