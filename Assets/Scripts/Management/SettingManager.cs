using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Button exitGameButton;

    private void Start()
    {
        exitGameButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame(){
        Debug.Log("game quit");
        Application.Quit();
    }
}
