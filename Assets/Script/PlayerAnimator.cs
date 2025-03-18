using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking"; //because Strings are bad way to handle attribute value
    private Animator animator;
    [SerializeField]private PlayerController playerController;

    private void Awake()
    {
        animator = GetComponent<Animator>();// can also use Serialized field and add to it
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, playerController.IsWalking());
    }
}
