using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GenderSelection : MonoBehaviour
{
    public GenderType genderType;
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(SelectGender);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(SelectGender);
    }
    public void SelectGender() 
    {
        GameManager.instance.characterdata.gender = genderType.ToString();
    }
}
