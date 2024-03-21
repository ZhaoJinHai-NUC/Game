using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject UItalk;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Button.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Button .activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            UItalk.SetActive(true);
        }
    }
}
