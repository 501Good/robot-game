using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAbilities : MonoBehaviour
{
    public PlayerController2D BaseCharacterPrefab;
    public PlayerController2D AdditionalCharacterPrefab;

    public Cinemachine.CinemachineVirtualCamera PlayerCamera;

    public bool AllowTransformation;

    private Animator anim;
    private bool isGrounded;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeType()
    {
        if (AdditionalCharacterPrefab.gameObject.activeSelf)
        {
            SwapCharacters(AdditionalCharacterPrefab, BaseCharacterPrefab);
        } 
        else
        {
            SwapCharacters(BaseCharacterPrefab, AdditionalCharacterPrefab);
        }
    }

    private void SwapCharacters(PlayerController2D char1, PlayerController2D char2)
    {
        var currentPos = gameObject.transform.position;
        char2.gameObject.transform.position = currentPos;
        char1.gameObject.SetActive(false);
        char2.gameObject.SetActive(true);
        PlayerCamera.Follow = char2.gameObject.transform;
        PlayerCamera.LookAt = char2.gameObject.transform;
        Events.SetCurrentCharacter(char2);
    }

    private IEnumerator ChangeTypeAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ChangeType();
    }

    private PlayerController2D GetActivePlayer()
    {
        if (AdditionalCharacterPrefab.gameObject.activeSelf)
        {
            return AdditionalCharacterPrefab;
        }
        else
        {
            return BaseCharacterPrefab;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && AllowTransformation)
        {
            if (GetActivePlayer().GetGrounded())
            {
                anim.SetTrigger("Transforming");
                StartCoroutine(ChangeTypeAfterDelay(0.5f));
            }
        }
    }
}
