using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFish : MonoBehaviour
{
    Rigidbody2D rb;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void Move(Vector2 dir)
    {
        rb.AddForce(dir * Random.Range(200,270));
    }
}
