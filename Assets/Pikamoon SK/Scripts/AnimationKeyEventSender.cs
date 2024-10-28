using UnityEngine;
using Pikamoon.Controller;
public class AnimationKeyEventSender : MonoBehaviour
{
    Combat combat;
    ShootingManager shootingManager;
    private void Start()
    {
        combat = GetComponentInParent<Combat>();
        shootingManager = GetComponentInParent<ShootingManager>();
    }

    void GiveImapact()
    {

    }

    public void AllowCombo()
    {
        combat.ToggleNextComboAttckStatus(true);
    }

    public void DenyComboInput()
    {
        combat.ToggleNextComboAttckStatus(false);
    }

    public void Shoot()
    {
        shootingManager.ShootArrow();
    }
}
