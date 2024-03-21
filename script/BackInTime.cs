using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackInTime : MonoBehaviour
{
    public float reTime;
    private Vector3 recordedPosition;
    private bool positionRecorded = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!positionRecorded)
            {
                reTime =2f;
                recordedPosition = transform.position;
                Debug.Log("Position recorded: " + recordedPosition);
                positionRecorded = true;
            }
            
        }
        reTime -= Time.deltaTime;
        if (positionRecorded && reTime < 0)
        {
            transform.position = recordedPosition;
            Debug.Log("Returned to recorded position: " + recordedPosition);
            positionRecorded = false;
        }
        
    }
}
