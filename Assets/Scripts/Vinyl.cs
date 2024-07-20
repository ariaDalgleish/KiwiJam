using UnityEngine;

public class Vinyl : MonoBehaviour
{
    public AudioClip audioClip; // The audio clip associated with this vinyl
    public Animator animator; // The animator component for the vinyl

    public void PlayAnimation()
    {
        animator.SetBool("IsSpinning", true);
    }

    public void StopAnimation()
    {
        animator.SetBool("IsSpinning", false);
    }
}
