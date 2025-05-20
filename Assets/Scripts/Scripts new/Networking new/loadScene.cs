using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class loadScene : MonoBehaviour
{
    

    public void LoadTheScene()
    {
        SceneManager.LoadScene("GamePlay");
    }
        
}
