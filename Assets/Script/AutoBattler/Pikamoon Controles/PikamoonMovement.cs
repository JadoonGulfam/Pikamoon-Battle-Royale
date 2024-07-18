using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PikamoonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    private float speed = 0.5f;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public bool MoveTowardsOpponent(GameObject nearestOpponent)
    {
        if (nearestOpponent == null) return false;

        Vector3 opponentPosition = nearestOpponent.transform.position;
        float distance = Vector3.Distance(transform.position, opponentPosition);

        if (distance < 1.5f)
        {
            return true;
        }

        Vector3 direction = (opponentPosition - transform.position).normalized;

        characterController.Move(direction * speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);
        }

        return false;
    }
}
