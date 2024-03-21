using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    public Transform backDoor;

    private bool isDoor;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoor&&Input.GetKeyDown (KeyCode.E) )
        {
            playerTransform.position  = backDoor.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag ("Player")
            && other.GetType().ToString ()=="UnityEngine.CapsuleCollider2D")
        {
            Debug.Log("角色进入门的范围"); 
            isDoor = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Debug.Log("角色离开门的范围");
            isDoor = false ;
        }
    }
}
