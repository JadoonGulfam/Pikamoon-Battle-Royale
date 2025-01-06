using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public PikamoonCommandHandler commandHandler; // Reference to the PikamoonCommandHandler

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Follow command
        {
           // commandHandler.ExecuteCommand(PikamoonCommand.Follow);
        }
        if (Input.GetKeyDown(KeyCode.Q)) // Attack command
        {
            Transform target = GetTargetEnemy(); // Get target enemy
            if (target != null)
            {
               // commandHandler.ExecuteCommand(PikamoonCommand.Attack, target);
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) // Roam command
        {
            //commandHandler.ExecuteCommand(PikamoonCommand.Roam);
        }
        if (Input.GetKeyDown(KeyCode.E)) // Release command
        {
           // commandHandler.ExecuteCommand(PikamoonCommand.Release);
        }
    }

    private Transform GetTargetEnemy()
    {
        // Implement logic to get target enemy based on player's selection
        return null;
    }
}
