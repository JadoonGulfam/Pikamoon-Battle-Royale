using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainPanel, loadingPanel, genderSelectionPanel;
    public Button startBtn;
    public Image loadingSprite;
    private bool isLoading=false;
    private void Start()
    {
        isLoading = true;
    }
    private void Update()
    {
        if (isLoading)
        {

            loadingSprite.fillAmount += Time.deltaTime * 0.2f;
            if (loadingSprite.fillAmount >= 0.9f)
            {
                loadingPanel.SetActive(false);
                genderSelectionPanel.SetActive(true);
                isLoading = false;
            }
        }
    }
}
