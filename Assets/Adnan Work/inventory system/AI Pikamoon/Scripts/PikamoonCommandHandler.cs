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
    public PikamoonInventory pikamoonInventory;
    private void Awake()
    {
        pikamoonAI = GetComponent<PikamoonAI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("built"))
        {
            print("captured");
            pikamoonInventory.AddPikamoon(this.gameObject);
        }
    }
    private void OnMouseDown()
    {
        //print("captured");
       // pikamoonInventory.AddPikamoon(this.gameObject);
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
