using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Enter");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        print("Stay");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        print("Exit");
    }
}
