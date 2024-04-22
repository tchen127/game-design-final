using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Crumble : MonoBehaviour
{
    // public Collider2D platformCollider;
    // public SpriteRenderer spriteRenderer;
    public Animator animator;

    [SerializeField]
    public float animTime;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void onCollisionEnter2D(Collision2D other)
    {
        Debug.Log("testing");

        if (other.gameObject.CompareTag("Player"))
        {
            animator.Play("Crumble_L");
            Debug.Log("testing 2");
            Destroy(gameObject, 2);
        }
    }

    // IEnumerator Crumble()
    // {
    //     animator.Play("Crumble_L");
    //     // yield return new WaitForSeconds(animTime);
    //     // Components(false); 
    //     // yield return new WaitForSeconds(animTime);
    //     // animator.Play("Crumble_2");
    //     yield return new WaitForSeconds(animTime);
    //     // Components(false);
    //     Destroy(gameObject, 1);
    // }

    // private void Components(bool state)
    // {
    //     spriteRenderer.enabled = state;
    //     platformCollider.enabled = state;
    // }
}
