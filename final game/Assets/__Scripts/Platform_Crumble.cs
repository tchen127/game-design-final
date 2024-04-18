using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Crumble : MonoBehaviour
{
    public Collider2D platformCollider;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [SerializeField]
    public float animTime;

    private void onCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Crumble();
        }
    }

    IEnumerator Crumble()
    {
        animator.Play("Crumble_test");
        // yield return new WaitForSeconds(animTime);
        // Components(false); 
        // yield return new WaitForSeconds(animTime);
        // animator.Play("Crumble_2");
        yield return new WaitForSeconds(animTime);
        Components(false);
        Destroy(gameObject);
    }

    private void Components(bool state)
    {
        spriteRenderer.enabled = state;
        platformCollider.enabled = state;
    }
}
