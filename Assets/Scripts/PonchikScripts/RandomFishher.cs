using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFishher : MonoBehaviour
{
    [SerializeField] private GameObject[] fishes;

    [SerializeField] private GameObject[]vf = new GameObject[2];


    [SerializeField] private float time;
    private float lastTime = 0;
    private void Update()
    {
        if(Time.time >= lastTime + time)
        {
            for(int i =0; i < Random.Range(1,4); i++)
            {
                var s = Instantiate(fishes[Random.Range(0, fishes.Length)]);
                var rand = Random.Range(0,100);

                Vector2 vect;

                if (rand %2 == 0)
                {
                    vect = vf[0].transform.position;
                    s.SendMessage("Move", Vector2.right);
                    vect.y = Random.Range(-16, 16);
                    s.transform.position = vect;
                }
                else
                {
                    vect = vf[1].transform.position;
                    s.SendMessage("Move", -Vector2.right);
                    vect.y = Random.Range(-16, 16);
                    s.transform.position = vect;
                    s.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            lastTime = Time.time;
        }
    }
}
