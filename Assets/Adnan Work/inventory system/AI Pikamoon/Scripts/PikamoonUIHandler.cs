using UnityEngine;

public class PikamoonUIHandler : MonoBehaviour
{
    public PikamoonCommandHandler commandHandler;

    public void OnFollowButtonClick()
    {
        commandHandler.ExecuteCommand(PikamoonCommand.Follow);
    }

    public void OnAttackButtonClick(Transform target)
    {
        commandHandler.ExecuteCommand(PikamoonCommand.Attack, target);
    }

    public void OnRoamButtonClick()
    {
        commandHandler.ExecuteCommand(PikamoonCommand.Roam);
    }

    public void OnReleaseButtonClick()
    {
        commandHandler.ExecuteCommand(PikamoonCommand.Release);
    }
}
