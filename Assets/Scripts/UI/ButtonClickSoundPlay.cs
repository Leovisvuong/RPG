using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSoundPlay : MonoBehaviour
{
    private Button button;
    private AudioSource buttonClick;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonClick = GameObject.Find("Button Click").GetComponent<AudioSource>();
    }

    private void Start()
    {
        button.onClick.AddListener(PLaySound);
    }

    private void PLaySound(){
        buttonClick.Play();
    }
}
