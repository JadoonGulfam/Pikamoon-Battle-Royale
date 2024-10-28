using UnityEngine;

public enum PikamoonCommand
{
    Follow,
    Attack,
    Roam,
    Release
}

public class PikamoonCommandHandler : MonoBehaviour
{
    private PikamoonAI pikamoonAI;

    private void Awake()
    {
        pikamoonAI = GetComponent<PikamoonAI>();
    }

    public void ExecuteCommand(PikamoonCommand command, Transform target = null)
    {
        if (pikamoonAI == null) return;

        switch (command)
        {
            case PikamoonCommand.Follow:
                pikamoonAI.FollowPlayer();
                break;
            case PikamoonCommand.Attack:
                pikamoonAI.AttackEnemy(target);
                break;
            case PikamoonCommand.Roam:
                pikamoonAI.StartRoaming();
                break;
            case PikamoonCommand.Release:
                pikamoonAI.ReleasePikamoon();
                break;
        }
    }
}
