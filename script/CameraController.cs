using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColl : MonoBehaviour
{
    public Transform target;
    public Transform farBackground, middleBackground,nearBackground;
    private Vector2 lastPos;

    public Vector2 minPosition;
    public Vector2 maxPosition;

    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = target.position;
        targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);

        transform.position = new Vector3(targetPos.x, targetPos.y, -10f);

        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        nearBackground.position += new Vector3(amountToMove.x * 0.3f, amountToMove.y * 0.3f, 0f);
        farBackground.position += new Vector3(amountToMove.x, amountToMove  .y, 0f);
        middleBackground.position += new Vector3(amountToMove.x * 0.6f, amountToMove.y * 0.6f, 0f);
        

    lastPos = transform.position;
    }
    public void SetCamPosLimit(Vector2 minPos, Vector2 maxPos)
    {
        minPosition = minPos;
        maxPosition = maxPos;
    }
}
