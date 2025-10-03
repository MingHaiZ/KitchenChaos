using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string IS_WALKING = "IsWalking";
    [SerializeField] private Player player;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        } else
        {
            Debug.LogError("Player Animator Not Found!");
        }

        animator.SetBool(IS_WALKING, false);
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}