using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualtySetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   public void SetUpValue(int value)
    {
        print(value);
        QualitySettings.SetQualityLevel(value, true);
    }
}
