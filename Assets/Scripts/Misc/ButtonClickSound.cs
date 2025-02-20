using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    private Button button;
    private AudioSource buttonSound;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonSound = GameObject.Find("Button Click").GetComponent<AudioSource>();
    }

    private void Start()
    {
        button.onClick.AddListener(PlaySound);
    }

    private void PlaySound(){
        buttonSound.Play();
    }
}
