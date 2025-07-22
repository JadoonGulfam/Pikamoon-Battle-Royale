using UnityEngine;

namespace Pikamoon.Controller
{
    public class WeaponDataSO : ItemDataSO
    {
        [Header("Weapon Data")]
        [Space]

        public WeaponType Type;

        public WeaponHoldingPointType HoldingPointType;

        public WeaponRestingPointType restingPointType;

        public int InitialHealth;

        public Sprite AimIcon;
        [Space]
        public AnimatorOverrideController AnimOC;
        public int AnimSpeed;
    }
}
