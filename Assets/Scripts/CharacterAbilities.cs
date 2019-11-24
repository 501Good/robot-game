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

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeType()
    {
        if (AdditionalCharacterPrefab.gameObject.activeSelf)
        {
            var currentPos = gameObject.transform.position;
            BaseCharacterPrefab.gameObject.transform.position = currentPos;
            AdditionalCharacterPrefab.gameObject.SetActive(false);
            BaseCharacterPrefab.gameObject.SetActive(true);
            PlayerCamera.Follow = BaseCharacterPrefab.gameObject.transform;
            PlayerCamera.LookAt = BaseCharacterPrefab.gameObject.transform;
        } else
        {
            var currentPos = gameObject.transform.position;
            AdditionalCharacterPrefab.gameObject.transform.position = currentPos;
            BaseCharacterPrefab.gameObject.SetActive(false);
            AdditionalCharacterPrefab.gameObject.SetActive(true);
            PlayerCamera.Follow = AdditionalCharacterPrefab.gameObject.transform;
            PlayerCamera.LookAt = AdditionalCharacterPrefab.gameObject.transform;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && AllowTransformation)
        {
            anim.SetTrigger("Transforming");
            ChangeType();
        }
    }
}
