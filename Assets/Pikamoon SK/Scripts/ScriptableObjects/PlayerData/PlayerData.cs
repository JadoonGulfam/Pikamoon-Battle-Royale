using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Pikamoon/Player/Create Player")]
public class PlayerData : ScriptableObject
{
    [Header("Locomotion")]
    [Space]
    public float WalkSpeed;
    public float RunSpeed;
    public float SprintSpeed;

    [Header("Jump Setting")]
    [Space]
    public float JumpHeight;
}
