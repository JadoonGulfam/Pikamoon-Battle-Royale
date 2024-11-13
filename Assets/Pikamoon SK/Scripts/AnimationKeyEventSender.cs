using UnityEngine;
using Pikamoon.Controller;
public class AnimationKeyEventSender : MonoBehaviour
{
    Combat combat;
    Shooting shootingManager;
    private void Start()
    {
        combat = GetComponentInParent<Combat>();
        shootingManager = GetComponentInParent<Shooting>();
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
