using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePowerSctipt : MonoBehaviour
{
    private void OnEnable()
    {
        Events.ChangeAllowTransformation(true);
    }
}
