using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    public Transform cam;
    private CharacterController characterController;
    private Animator animator;
    public float speed;
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public float walkSpeed = 1f;
    public float runSpeed = 2f;
    // Gravity and jump settings
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;
    private Vector3 velocity;
    public bool isGrounded;

    // Animation parameters
    private float animationBlend;

    AudioSource characterSound;
    public AudioClip roadFootStepSound;
    public AudioClip mudFootStepSound;
    public AudioClip grassFootStepSound;
    public AudioClip jumpSound;

  //  private Terrain terrain;
    private Vector3 terrainPos;
    private string currentTextureName;

    // Footstep sound control
    private float footstepInterval = 0.5f; // Adjust interval as needed
    private float footstepTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        characterSound = GetComponent<AudioSource>();

      //  terrain = Terrain.activeTerrain;
    //    terrainPos = terrain.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("Grounded", true); // Landed
            animator.ResetTrigger("JumpStart");
            animator.SetTrigger("JumpLand"); // Land animation
            animator.SetBool("FreeFall", false);
        }
        else if (!isGrounded && velocity.y < 0)
        {
            // Player is falling
            animator.SetBool("FreeFall", true);
        }
        //   if (isGrounded)
        //    animator.SetBool("Grounded", false);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveInDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveInDir.normalized * targetSpeed * Time.deltaTime);

            // Calculate animation blend value based on speed
            animationBlend = Mathf.Clamp01(direction.magnitude) * (targetSpeed == runSpeed ? 2f : 1f);

        }
        else
        {
            // Idle
            animationBlend = 0;
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("JumpStart"); // Start jump
            animator.SetBool("Grounded", false); // In the air
            animator.SetBool("IsFalling", false); // Reset falling state
            isGrounded = false;

        }      
        //if (chkforGround)
        //{
        //    animator.SetBool("Grounded", true);

        //}
        // Update animator with blend value
        animator.SetFloat("MotionSpeed", animationBlend);
        // animator.SetFloat("JumpSpeed", velocity.y); // Update vertical speed for in-air animation
        DetectTerrainTexture();
    }
    void DetectTerrainTexture()
    {
    //    Vector3 playerPos = transform.position;
      //  Vector3 terrainCoord = playerPos - terrainPos;
      //  Vector3 mapCoord = new Vector3(terrainCoord.x / terrain.terrainData.size.x, 0, terrainCoord.z / terrain.terrainData.size.z);

    //    int xMap = Mathf.RoundToInt(mapCoord.x * terrain.terrainData.alphamapWidth);
     //   int zMap = Mathf.RoundToInt(mapCoord.z * terrain.terrainData.alphamapHeight);

      //  float[,,] splatmapData = terrain.terrainData.GetAlphamaps(xMap, zMap, 1, 1);

     //   float[] textureMix = new float[splatmapData.GetUpperBound(2) + 1];
    //    for (int i = 0; i < textureMix.Length; i++)
     /*   {
            textureMix[i] = splatmapData[0, 0, i];
        }

        int textureIndex = 0;
        float maxMix = 0;

        for (int i = 0; i < textureMix.Length; i++)
        {
            if (textureMix[i] > maxMix)
            {
                textureIndex = i;
                maxMix = textureMix[i];
            }
        }

        string newTextureName = terrain.terrainData.terrainLayers[textureIndex].name;

        if (newTextureName != currentTextureName)
        {
            currentTextureName = newTextureName;
            ChangeAnimationAndSound(newTextureName);
        }*/
    }
    void PlayFootstepSound()
    {
        if (characterSound.clip != null && isGrounded && characterController.velocity.magnitude > 0.1f)
        {
            characterSound.Play();
        }
    }
    void ChangeAnimationAndSound(string textureName)
    {
        switch (textureName)
        {
            case "Dirt_02":
               // animator.SetTrigger("WalkOnRoad");
                characterSound.clip = mudFootStepSound;
                Debug.Log("Dirt_02");
                break;
            case "Grass_01":
               // animator.SetTrigger("WalkOnMud");
                characterSound.clip = grassFootStepSound;
                Debug.Log("Grass_01");
                break;
            case "Dirt_01":
               // animator.SetTrigger("WalkOnGrass");
                characterSound.clip = mudFootStepSound;
                Debug.Log("Dirt_01");
                break;
            case "Dirt_Road":
               // animator.SetTrigger("WalkOnGrass");
                characterSound.clip = mudFootStepSound;
                Debug.Log("Dirt_Road");
                break;
            case "Cliff":
               // animator.SetTrigger("WalkOnGrass");
                characterSound.clip = roadFootStepSound;
                Debug.Log("Cliff");
                break;
            default:
               // animator.SetTrigger("WalkDefault");
                Debug.Log("WalkDefault");
                characterSound.clip = null;
                break;
        }
    }
    bool chkforGround;
    private void JumpAnimate()
    {
        chkforGround = true;
        // animator.SetBool("Jump", true); // Start jump

       // yield return new WaitUntil(()=> isGrounded == true);
       //  yield return new WaitForSeconds(10.5f);
       // animator.SetBool("Grounded", true);
        // animator.SetBool("Grounded", false);
        Debug.Log("Princ was rescued!");
    }
    private void OnLand()
    {
        //  if (animationEvent.animatorClipInfo.weight > 0.5f)
        // {
        chkforGround = false;
        animator.SetBool("Grounded", false);
      //  Debug.Log("disbale jump please");
            //   AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
      //  }
    }
    private void checkOnGround()
    {
        //  if (animationEvent.animatorClipInfo.weight > 0.5f)
        // {
        animator.SetBool("Grounded", true);
        Debug.Log("disbale jump please");
        //   AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
        //  }
    }
    private void OnFootstep()
    {
        characterSound.Play();
    }
    private void OnJump()
    {
        characterSound.PlayOneShot(jumpSound);
    }

}
