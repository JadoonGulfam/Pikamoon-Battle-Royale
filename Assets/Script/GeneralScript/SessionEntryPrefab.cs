using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionEntryPrefab : MonoBehaviour
{

    public Text sessionName, playerCount;
    public Button joinButton;
    // Start is called before the first frame update
    void Awake()
    {
        joinButton.onClick.AddListener(JoinListner);
    }
    private void Start()
    {
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
    }
    void JoinListner()
    {

        GameManager.instance.ConnectToSession(sessionName.text);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
