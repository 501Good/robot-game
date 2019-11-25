﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeTrigger : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Events.ChangeToCamera(cam);
    }
}
