using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshButton : MonoBehaviour
{
    //public Button refreshButton;
    // Start is called before the first frame update
    void Start()
    {

       // refreshButton.onClick.AddListener(Refresh);
    }

    private void Refresh()
    {
       // StartCoroutine(RefreshWait());
      //  throw new NotImplementedException();
    }

    //IEnumerator RefreshWait()
    //{
    //    refreshButton.interactable = false;
    //    GameManager.instance.RefreshSessionListUI();
    //    yield return new WaitForSeconds(3f);
    //    refreshButton.interactable = true;
    //}
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
