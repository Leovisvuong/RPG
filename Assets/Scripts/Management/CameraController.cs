using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start(){
        SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow(){
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if(PlayerController.Instance) cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
}
