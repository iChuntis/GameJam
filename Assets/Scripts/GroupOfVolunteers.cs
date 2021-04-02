using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GroupOfVolunteers : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Text count;

    [SerializeField] private float speed;

    private Vector2 direction;

    private bool moving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(in int count , Vector2 point_B )
    {
        this.count.text = count.ToString();
        moving = true;
        StartCoroutine(moveTo(point_B));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "People")
        {

        }      
    }

    private IEnumerator moveTo(Vector2 point_B)
    {
        while (moving)
        {

        }


        yield return null;
    }
}
