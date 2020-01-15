using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    private BoxCollider2D _trigger;
    private NPC _npc;
    void Start()
    {
        _trigger = GetComponent<BoxCollider2D>();
        _npc = GetComponent<NPC>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _npc.TriggerDialogue();
        }
    }
}
