using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerTeleportHandler : NetworkBehaviour
{
    [Header("Teleport Settings")]
    public KeyCode teleportKey = KeyCode.T;
    public Vector3 teleportPosition = new Vector3(-305.019257f, 18.4018574f, 235.47197f);

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // In Shared Mode, each peer controls its own StateAuthority object
        if (Object.HasStateAuthority && Input.GetKeyDown(teleportKey))
        {
            Debug.Log("[Fusion] Teleport key pressed (Shared Mode).");
            PerformTeleport(teleportPosition);

            // Notify others (optional visual sync)
            RPC_SyncTeleport(teleportPosition);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("teleportPoint"))
        {
            Debug.Log("[Fusion] Teleport key pressed (Shared Mode).");
            PerformTeleport(teleportPosition);

            // Notify others (optional visual sync)
            RPC_SyncTeleport(teleportPosition);
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_SyncTeleport(Vector3 targetPosition)
    {
        PerformTeleport(targetPosition);
        Debug.Log($"[Fusion] Teleported player to {targetPosition}");
    }

    private void PerformTeleport(Vector3 position)
    {
        if (characterController != null)
        {
            characterController.enabled = false;
            transform.position = position;
            characterController.enabled = true;
        }
        else
        {
            transform.position = position;
        }
    }
}
