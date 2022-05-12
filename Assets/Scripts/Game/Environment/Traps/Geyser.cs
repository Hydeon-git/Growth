using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    // El tiempo que estará activo
    public float activeTime;

    // El tiempo que estará desactivado
    public float timeStopped;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ProcessColliderActivation());
    }

    private IEnumerator ProcessColliderActivation()
    {
        GetComponent<Collider2D>().enabled = true;
        animator.SetBool("GeiserCum", true);
        yield return new WaitForSeconds(activeTime);
        GetComponent<Collider2D>().enabled = false;
        animator.SetBool("GeiserCum", false);
        yield return new WaitForSeconds(timeStopped);
        StartCoroutine(ProcessColliderActivation());
    }
}