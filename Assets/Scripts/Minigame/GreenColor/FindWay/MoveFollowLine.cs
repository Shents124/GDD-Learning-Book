using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFollowLine : MonoBehaviour
{
    public LineController wayControl;

    public float speed = 10f;

    bool startMovement = false;

    bool startDraw = false;

    private Vector3[] positionMove;

    int moveIndex = 0;


    public void StartDraw()
    {
        startDraw = true;
    }

    public void StopDraw()
    {
        startDraw = false;
        startMovement = false;
        wayControl.StopDraw();
    }

    private void OnMouseDown()
    {
        if (!startDraw)
            return;
        wayControl.StartDraw();
    }

    private void OnMouseUp()
    {
        if (!startDraw)
            return;
        wayControl.StopDraw();
    }

    public void StartMove()
    {
        positionMove = wayControl.getPoints().ToArray();
        startMovement = true;
        moveIndex = 0;
    }

    void Update()
    {
        if(startMovement)
        {
            Vector2 currentPos = positionMove[moveIndex];
            transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);

            //rotate
            Vector2 dir = currentPos - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.normalized.x, dir.normalized.y);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90f), speed);

            float distance = Vector2.Distance(transform.position, currentPos);
            if(distance <= 0.05f)
            {
                moveIndex++;
            }

            if(moveIndex >= positionMove.Length)
            {
                startMovement = false;
            }
        }
    }
}
