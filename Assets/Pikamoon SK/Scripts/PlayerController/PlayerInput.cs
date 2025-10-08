using System.Collections;
using UnityEngine;


namespace Pikamoon.Controller
{
    public delegate void OnBtnClicked();
    public delegate void OnBtnDown();
    public delegate void OnBtnUp();

    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput Instance
        {
            get { return s_Instance; }
        }

        protected static PlayerInput s_Instance;
        public OnBtnClicked onAttack1_Clicked;

        public OnBtnDown onAttack1_Down;
        public OnBtnUp   onAttack1_Up;


        public OnBtnClicked onAttack2_Clicked; 

        public OnBtnDown onAttack2_Down;
        public OnBtnUp   onAttack2_Up;

        public OnBtnDown onSprint_Down;
        public OnBtnUp   onSprint_Up;

        public OnBtnDown onCrouch_Down;
        public OnBtnUp   onCrouch_Up;

        public OnBtnDown onWalk_Down;
        public OnBtnUp   onWalk_Up;

        public OnBtnDown onPrimaryWeaponSelect_Down;
        public OnBtnDown onSecondaryWeaponSelect_Down;
        public OnBtnDown onTertiaryWeaponSelect_Down;

        public OnBtnDown onJump_Down;
        
        public OnBtnDown onWeaponDrop_Down;

        public OnBtnDown onInventoryShow_Down;

        public OnBtnDown onPick_Down;

        public OnBtnDown onCapture_Down;
        public OnBtnUp   onCapture_Up;

        public OnBtnDown onMap_Down;

        public OnBtnDown onEscape;

        [HideInInspector]
        //public bool isSprinting;
        //public bool isCrouching;
        //public bool isSliding;
        [SerializeField] float horizontal;
        [SerializeField] float vertical;

        [SerializeField] bool jump;
        [SerializeField] float jumpVelocity;
        [Space]
        public bool AllowInputFlagWhileUIEnabled;
        [Space]
        //[SerializeField] PlayerController m_Controller;


        Coroutine m_AttackWaitCoroutine;
        public float Vertical
        {
            get
            {
                return vertical;
            }
        }
        public bool isMoving
        {
            get
            {
                return vertical != 0 || horizontal != 0;
            }
        }
        public float Horizontal
        {
            get
            {
                return horizontal;
            }
        }

        public bool JumpInput
        {
            get { return jump; }
            set { jump = value; }
        }


        public float JumpVelocity
        {
            get { return jumpVelocity; }
            set { jumpVelocity = value; }
        }


        void Awake()
        {
            if (s_Instance == null)
                s_Instance = this;
            else if (s_Instance != this)
                throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");
        }



        void Update()
        {
            vertical   = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                onEscape?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                onInventoryShow_Down?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                onJump_Down?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                onMap_Down?.Invoke();
            }

            if (!AllowInputFlagWhileUIEnabled)
                return;




            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                onSprint_Down?.Invoke();
            }

            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                onSprint_Up?.Invoke();
            }


            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                onCrouch_Down?.Invoke();

            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                onCrouch_Up?.Invoke();
               
            }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                onWalk_Down?.Invoke();
            }
            
            if(Input.GetKeyUp(KeyCode.Q))
            {
                onWalk_Up?.Invoke();
            }


            if (Input.GetKeyDown(KeyCode.C))
            {
                onCapture_Down?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                onCapture_Up?.Invoke();
            }



            if (Input.GetKeyDown(KeyCode.G))
            {
                onWeaponDrop_Down?.Invoke();
            }
           


            if (Input.GetKeyDown(KeyCode.E))
            {
                onPick_Down?.Invoke();
            }


            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                onPrimaryWeaponSelect_Down?.Invoke();
            }



            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                onSecondaryWeaponSelect_Down?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                onTertiaryWeaponSelect_Down?.Invoke();
            }

            if (Input.GetMouseButtonDown(0))
            {
                onAttack1_Clicked?.Invoke();
                onAttack1_Down?.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                onAttack1_Up?.Invoke();
            }


            if (Input.GetMouseButtonDown(1))
            {
                onAttack2_Clicked?.Invoke();
                onAttack2_Down?.Invoke();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                onAttack2_Up?.Invoke();
            }
        }

        //IEnumerator Attack1Wait()
        //{
        //    attack1 = true;

        //    yield return new WaitForSeconds(m_AttackInputWait);

        //    attack1 = false;
        //}

        //IEnumerator Attack2Wait()
        //{
        //    attack1 = true;

        //    yield return new WaitForSeconds(m_AttackInputWait);

        //    attack1 = false;
        //}




        //public void ToggleSprinting()
        //{
        //    if(!isCrouching)
        //        if(!isSprinting && !m_Controller.IsInAttack)
        //        {
        //            isSprinting = true;
        //            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Sprint);
        //        }
        //        else
        //        {
        //            isSprinting = false;
        //            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Default);
        //        }
        //}

        //public void ToggleCrouching()
        //{
        //    if (!isSprinting)
        //        if (!isCrouching)
        //        {
        //            isCrouching = true;
        //            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Crouch);
        //        }
        //        else
        //        {
        //            isCrouching = false;
        //            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Default);
        //        }
        //}

    }
}