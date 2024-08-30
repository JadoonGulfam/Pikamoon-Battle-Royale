using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHndler : MonoBehaviour
{
    public PikamoonBattleAnimationController pikamoonBattleAnimationController;
    public enum CharacterType
    {
        Player,
        AI
    }
    public void Initialize(bool isAI)
    {
        characterType = isAI ? CharacterType.AI : CharacterType.Player;
    }
    public CharacterType characterType;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Attack"))
        {
            if(characterType== CharacterType.AI)
            {
                if(other.gameObject.GetComponent<AttackParticles>().IsPlayerCast())
                {
                    pikamoonBattleAnimationController.PlayHitAnimation();
                    other.gameObject.SetActive(false);
                }
            }
            else if(characterType == CharacterType.Player)
            {
                if (other.gameObject.GetComponent<AttackParticles>().IsAICast())
                {
                    pikamoonBattleAnimationController.PlayHitAnimation();
                    other.gameObject.SetActive(false);
                }
            }
            else
            {
                print("collide with team attack");
            }
            
        }
    }
}
