using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikUp : MonoBehaviour
{
    public string name;
    [SerializeField] Inventory inventory;
    private void OnMouseDown()
    {
        print("clicked" + this.gameObject.name);
        inventory.CollectItem(name);
        Destroy(this.gameObject);
    }

    private void OnMouseExit()
    {
        print("exict" + this.gameObject.name);
    }
    private void OnMouseEnter()
    {
        print("enter" + this.gameObject.name);
    }
}
