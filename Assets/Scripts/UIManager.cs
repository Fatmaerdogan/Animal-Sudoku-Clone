using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject loseText,winMenu;
    [SerializeField] Slider difficultySlider;
    public void StartButtonClicked()
    {
        PlayerSettings.difficulty = (int)difficultySlider.value;
        SceneManager.LoadScene(1);
    }

    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
    public void CheckComplete()
    {
        if (Board.instance.CheckGrid())
        {
            winMenu.SetActive(true);
        }
        else
        {
            loseText.SetActive(true);
        }
    }
}
