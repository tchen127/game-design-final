// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;


//!!!! DELETE THIS FILE: Replaced by platformMom and platformChild scripts!


// public class myEventTriggerOnEnter : MonoBehaviour
// {
//     [Header("Custom Event")]
//     public UnityEvent myEvents;

//     // public Collider2D platformCollider;
//     // public SpriteRenderer spriteRenderer;
//     // public Animator animator;

//     // [SerializeField]
//     // public float animTime;

//     // public void Start()
//     // {
//     //     animator = GetComponent<Animator>();
//     //     animTime = 0;
//     // }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             if (myEvents == null)
//             {
//                 print("event trigger was null");
//             }
//             else
//             {
//                 Debug.Log("trigger activated" + myEvents);
//                 myEvents.Invoke();

//             }
//             Destroy(gameObject, 1);
//         }


//         // if (other.CompareTag("Player"))
//         // {
//         //     // animator.enabled = true;
//         //     // animTime = 1;

//         //     // Debug.Log(other.gameObject.name + "");
//         //     animator.Play("Crumble_L");
//         //     Components(false);
//         //     Destroy(gameObject);
//         // }
//     }

//     // IEnumerator Crumble()
//     // {
//     //     animator.Play("Crumble_L");
//     //     // yield return new WaitForSeconds(animTime);
//     //     // Components(false); 
//     //     // yield return new WaitForSeconds(animTime);
//     //     // animator.Play("Crumble_2");
//     //     yield return new WaitForSeconds(animTime);
//     //     Components(false);
//     //     Destroy(gameObject);
//     // }

//     // void Components(bool state)
//     // {
//     //     spriteRenderer.enabled = state;
//     //     platformCollider.enabled = state;
//     // }
// }
