using UnityEngine;


namespace Pikamoon.Controller
{

    [CreateAssetMenu(fileName = "New Fist Move", menuName = "Pikamoon/Combat/Combo Move/Create New Fist Move")]
    public class FistMoveSO : ComboMoveSO
    {
        public CombatMoveEffectPoint[] combatMoveEffectPoint;
        public CombatMoveType combatMoveType;
    }

}