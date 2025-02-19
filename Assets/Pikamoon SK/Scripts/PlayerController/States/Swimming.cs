using System.Xml.Serialization;
using UnityEngine;
namespace Pikamoon.Controller
{
    public class Swimming : State
    {
        public LayerMask waterLayer;
        public float chestDisToFeet;

        public float UpForce;

        bool _inWater;
        bool _isNormalSwim;
        bool _isSwimming;

        float _verticalForce;

        public bool IsInWater
        {
            get
            {
                return _inWater;
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public override void Initialize()
        {
            base.Initialize();

            playerInput.onSprint_Down += EnableFastSwim;
            playerInput.onSprint_Up   += DisbleFastSwim;
        }

        // Update is called once per frame
        void Update()
        {
            if (Controller.MP_Setup != null && !Controller.MP_Setup.isMinePlayer)
                return;


            _inWater =  Physics.CheckSphere(this.transform.position, 0.5f, waterLayer);

            if (!_inWater)
            {
                return;
            }

            if (IsInEnoughDeepToSwim())
            {
                if(!_isSwimming)
                {
                    OnStart();
                }

                HandleSwimmingSpeed();

                MovementAndRotationHandler();

                HandleAnimation();

                CheckPlayerFloating();
            }
            else
            {
                if (_isSwimming)
                {
                    OnEnd();
                }

            }
        }

        void CheckPlayerFloating()
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position + (Vector3.up * 100), Vector3.down, out hit, 120, waterLayer))
            {
                float dist = Vector3.Distance(hit.point, this.transform.position);
                if (dist > chestDisToFeet)
                {
                    _verticalForce = UpForce;
                }
                else
                {
                    _verticalForce = 0;
                }
            }
        }

        bool IsInEnoughDeepToSwim()
        {
            Vector3 shoulderPosition = transform.position + Vector3.up * chestDisToFeet;

            if (Physics.CheckSphere(shoulderPosition, 0.1f, waterLayer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void HandleSwimmingSpeed()
        {
            if (playerInput.isMoving)
            {
                Controller.ChangeSpeed(_isNormalSwim ? Controller.PlayerData.SwimmingNormalSpeed : Controller.PlayerData.SwimmingFastSpeed,
                _isNormalSwim ? 1f : 2f);

            }
            else
            {
                _isNormalSwim = true;
                Controller.ChangeSpeed(0f, 0f);
            }
        }
        void HandleAnimation()
        {
            AC.PAnimator.SetFloat(AC.Parameters.Speed.Hash, Controller.AnimSpeed);
        }

        void MovementAndRotationHandler()
        {
            AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, 1);

            Vector3 direction = Controller.GetDirectionAccordingToCameraWhenMoving();

            Controller.RotatePlayerTowardDirection(direction, Controller.TurnSmoothTime);

            // Always apply vertical velocity (for gravity or jumping)
            Vector3 finalMove = new Vector3(direction.x * Controller.Speed, _verticalForce, direction.z * Controller.Speed);

            // Move the character based on calculated velocity and speed
            Controller.Move(finalMove);
        }


        void ToggleFastSwim()
        {
            _isNormalSwim = !_isNormalSwim;

            Controller.ChangeSpeed(_isNormalSwim ? Controller.PlayerData.SwimmingNormalSpeed : Controller.PlayerData.SwimmingFastSpeed, _isNormalSwim ? 1f : 2f);
        }

        void EnableFastSwim()
        {
            if (Controller.CurrentPlayerState != StateType.Swimming)
                return;

            _isNormalSwim = false;
        }

        void DisbleFastSwim()
        {
            if (Controller.CurrentPlayerState != StateType.Swimming)
                return;

            _isNormalSwim = true;
        }



        public override void OnEnd()
        {
            Controller.CurrentPlayerState = StateType.Locomtion;
            _isSwimming = false;
            Controller.IgnoreGravity = false;
            Controller.IsSwimming = false;

            AC.PAnimator.SetBool(AC.Parameters.isSwim.Hash, false);
        }

        public override void OnStart()
        {
            Controller.CurrentPlayerState = StateType.Swimming;
            Controller.IgnoreGravity = true;
            Controller.IsSwimming = true;
            _verticalForce = 0;
            _isNormalSwim = true;
            _isSwimming = true;

            AC.PAnimator.SetBool(AC.Parameters.isSwim.Hash, true);
        }

        public override void OnUpdate()
        {
        }

        //public void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.layer == waterLayer)
        //    {
        //        _inWater = true;
        //    }
        //}

        //public void OnTriggerExit(Collider other)
        //{
        //    if (other.gameObject.layer == waterLayer)
        //    {
        //        _inWater = false;
        //    }
        //}

    }
}