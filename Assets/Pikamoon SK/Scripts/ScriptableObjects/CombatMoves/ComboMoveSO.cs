using UnityEngine;


namespace Pikamoon.Controller
{


    [CreateAssetMenu(fileName = "New Move", menuName = "Pikamoon/Combat/Create new Move")]
    public class ComboMoveSO : ScriptableObject
    {
        public CombatMoveEffectPoint[] combatMoveEffectPoint;
        public CombatMoveType combatMoveType;
        [Space]
        public float Damage;
    }
}