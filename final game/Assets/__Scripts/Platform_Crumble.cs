// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// !!!! DELETE THIS FILE: Replaced by platformMom and platformChild scripts!

// public class Platform_Crumble : MonoBehaviour
// {

//     // public Collider2D platformCollider;
//     // public SpriteRenderer spriteRenderer;
//     public Animator animator;

//     [SerializeField]
//     public float animTime;

//     void Start()
//     {
//         animator = gameObject.GetComponent<Animator>();
//     }

//     public void OnCollisionStay2D(Collision2D other)
//     {
//         // Debug.Log("play animation");

//         if (other.gameObject.CompareTag("Player"))
//         {
//             if (gameObject.CompareTag("Crumble_left"))
//             {
//                 animator.Play("Crumble_L");
//                 Debug.Log("crumble left animation played");
//             }
//             else if (gameObject.CompareTag("Crumble_middle"))
//             {
//                 animator.Play("Crumble_M");
//             }
//             else if (gameObject.CompareTag("Crumble_right"))
//             {
//                 animator.Play("Crumble_R");
//             }
//             else
//             {
//                 Debug.Log("no crumble");
//             }

//             Destroy(gameObject, 1);
//         }
//     }

//     // IEnumerator Crumble()
//     // {
//     //     animator.Play("Crumble_L");
//     //     // yield return new WaitForSeconds(animTime);
//     //     // Components(false); 
//     //     // yield return new WaitForSeconds(animTime);
//     //     // animator.Play("Crumble_2");
//     //     yield return new WaitForSeconds(animTime);
//     //     // Components(false);
//     //     Destroy(gameObject, 1);
//     // }

//     // private void Components(bool state)
//     // {
//     //     spriteRenderer.enabled = state;
//     //     platformCollider.enabled = state;
//     // }
// }
