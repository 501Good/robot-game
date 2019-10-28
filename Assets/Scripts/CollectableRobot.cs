using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableRobot : MonoBehaviour
{
    public int RobotType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var characterAbilities = collision.gameObject.GetComponent<CharacterAbilities>();
        characterAbilities.SetType(RobotType);
        Destroy(this.gameObject);
    }


}
