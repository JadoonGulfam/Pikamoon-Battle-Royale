using UnityEngine;
using DG.Tweening;
public class DummyInventory : MonoBehaviour
{

    public Transform InventoryPanelParent;


    bool isAnimBusy;
    bool isInventoryOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isInventoryOpen = false;
        isAnimBusy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.I))
        {
            ToggleInventory();
        }
    }


    public void ToggleInventory()
    {
        if (isAnimBusy)
            return;

        isAnimBusy = true;

        isInventoryOpen = !isInventoryOpen;

        if (isInventoryOpen)
        {
            InventoryPanelParent.DOLocalMoveX(-820, .75f).SetRelative().OnComplete(AnimComplete);
        }
        else
        {

            InventoryPanelParent.DOLocalMoveX(820, .75f).SetRelative().OnComplete(AnimComplete);
        }
    }


    void AnimComplete()
    {
        isAnimBusy = false;
    }

}

