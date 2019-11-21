using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDestroyThis : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
            Destroy(this.gameObject);
    }
}
