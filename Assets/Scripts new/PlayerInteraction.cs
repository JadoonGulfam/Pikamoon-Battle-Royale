using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] GameObject nftpanel;
    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            nftpanel.SetActive(true);
            interactable.Interact();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Interactable"))
        {
            Invoke("DisableNFTPanel", 2f);
        }
    }
    private void DisableNFTPanel()
    {
        nftpanel.SetActive(false);
    }
}
