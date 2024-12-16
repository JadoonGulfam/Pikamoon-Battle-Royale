using UnityEngine;

public class trailscript : MonoBehaviour
{

     public GameObject[] linerenderers;

     void Awake()
     {
         DisableLine();
     }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void EnableLine()
    {
        foreach(GameObject g in linerenderers )
        g.SetActive(true);

    }
    // Update is called once per frame
    public void DisableLine()
    {
        foreach(GameObject g in linerenderers )
        g.SetActive(false);

    }
}

