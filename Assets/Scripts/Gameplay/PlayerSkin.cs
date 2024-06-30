using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private Animator skinAnimator;

    public void PlayShootAnimation()
    {
        skinAnimator.SetTrigger("Shoot");
    }
    public void PlayWalkingAnimation(float speed)
    {
        skinAnimator.SetFloat("Speed", speed);
    }
}
