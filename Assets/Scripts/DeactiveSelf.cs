using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveSelf : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
