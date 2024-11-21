using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewSession : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_InputField SessionName;
    public TMP_InputField SessionMAXUser;
    public TMP_InputField SessionMaxTime;
    public Button CreateSessionButton;
    void Start()
    {
        CreateSessionButton.onClick.AddListener(getSessionstarus);
    }

    void getSessionstarus()
    {
        GameManager.instance.CreateCustomSession(SessionName.text,int.Parse(SessionMAXUser.text),int.Parse(SessionMaxTime.text));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
