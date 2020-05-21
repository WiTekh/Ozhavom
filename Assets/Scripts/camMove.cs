using System;
using UnityEngine;
using DG.Tweening;

public class camMove : MonoBehaviour
{
    void Update()
    {
        UpdateCamPos();
    }

    void UpdateCamPos()
    {
        Vector3 parent = transform.parent.position;
        Vector3 pos = transform.position;
        
        float X = parent.x;
        float Y = parent.y;

        if (mod(X, 19) < 9.5) {
            X = X - X % 19;
        }
        else {
            X = X - X % 19 + 19;
        }

        if (mod(Y, 12) < 6) {
            Y = Y - Y % 12;
        }
        else {
            Y = Y - Y % 12 + 12;
        }
        
        transform.DOMove(new Vector3(X, Y, pos.z), 1f);

        //Debug.Log(pos);
    }

    float mod(float x, float y)
    {
        if (x >= 0)
            return x % y;
        else
            return -1*(-x % y);
    }
}
