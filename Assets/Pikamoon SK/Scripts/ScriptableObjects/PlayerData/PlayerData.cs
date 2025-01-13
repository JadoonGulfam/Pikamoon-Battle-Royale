using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Pikamoon/Player/Create Player")]
public class PlayerData : ScriptableObject
{
    [Header("Locomotion")]
    [Space]
    public float Acceleration;
    public float WalkSpeed;
    public float RunSpeed;
    public float SprintSpeed;

    [Header("Tactical Sprint")]
    [Space]
    public float TacticalSprintSpeed;
    public float TacticalSprintStamina;
    public float TacticalSprintStaminaConsumption;
    public float TacticalSprintStaminaRecovery;

    [Header("Crouch")]
    [Space]
    public float CrouchWalkSpeed;
    public float CrouchRunSpeed;

    [Header("Slide")]
    [Space]
    public float SlideMaxSpeed;
    public float SildeInitialSpeedBoost;
    public float SlideMaxClimbableSlope;
    public float SlideSlopeContributionScale;
    public float SlideForcedDecelerationCoefficient;
    public float SlideImpacFactor;

    [Header("Jump")]
    [Space]
    public float JumpHeight;
    public float Gravity;
    public float JumpLandingBuffer;
    public float JumpCoyoteTime;
    public float JumpHangTime;

    [Header("Swimming")]
    [Space]
    public float SwimmingNormalSpeed;
    public float SwimmingFastSpeed;
    public float SwimmingMaxLedgeClimbHeight;



    [Header("Animation")]
    [Space]
    public float AnimationAcceleration;


}
