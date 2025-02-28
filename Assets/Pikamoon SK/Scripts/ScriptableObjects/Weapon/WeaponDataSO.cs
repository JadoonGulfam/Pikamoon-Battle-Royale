using UnityEngine;

namespace Pikamoon.Controller
{
    public class WeaponDataSO : ScriptableObject
    {
        public WeaponType Type;

        public WeaponHoldingPointType HoldingPointType;

        public WeaponRestingPointType restingPointType;

        public Sprite icon;

        public int InitialHealth;

        [Space]
        public AnimatorOverrideController AnimOC;
    }
}
