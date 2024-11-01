using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pikamoon.Controller;
public class AnimationKeyEventSender : MonoBehaviour
{
    Combat combat;

    private void Start()
    {
        combat = GetComponentInParent<Combat>();
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

}
