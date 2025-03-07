using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainPanel;
    public Button gameStartBtn;
    public Button exirGameBtn;
    private void Start()
    {
        exirGameBtn.onClick.AddListener(QuitGame);
        gameStartBtn.onClick.AddListener(StartGame);
    }
    private void Update()
    {

    }
    public void LoadCustomizationScene()
    {
        SceneManager.LoadSceneAsync("Customization");
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("lobby new");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#else
            Application.Quit(); // Quit the built game
#endif
    }
}
