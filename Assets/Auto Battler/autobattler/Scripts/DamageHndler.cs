using UnityEngine;

public class DamageHndler : MonoBehaviour
{
    public enum CharacterType
    {
        Player,
        AI
    }

    [SerializeField] private CharacterType characterType;
    [SerializeField] private PikamoonBattleAnimationController pikamoonBattleAnimationController;

    public void Initialize(bool isAI)
    {
        characterType = isAI ? CharacterType.AI : CharacterType.Player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Attack")) return;

        AttackParticles attackParticles = other.GetComponent<AttackParticles>();
        if (attackParticles == null) return;

        if ((characterType == CharacterType.AI && attackParticles.IsPlayerCast()) ||
            (characterType == CharacterType.Player && attackParticles.IsAICast()))
        {
            pikamoonBattleAnimationController.PlayHitAnimation();
            other.gameObject.SetActive(false);
        }
    }
}
