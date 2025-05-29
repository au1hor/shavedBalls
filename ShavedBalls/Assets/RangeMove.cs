using System;

using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class RangeMove : MonoBehaviour
{
    public GameObject range2;
    public Boolean apertou = false;
    public int dir = 1;
    public float speed = 10;
    private float dif = 5;
    private float rotaZ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        Vector3 euler1 = transform.eulerAngles;
        Vector3 eulerRange = range2.transform.rotation.eulerAngles;
        Vector3 distance = euler1 - eulerRange;
        rotaZ = distance.z;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dir *= -1;
            apertou = true;
            speed += dif;
            if (Mathf.Abs(rotaZ) < 10 )
            {
                Debug.Log("acertou");
                Debug.Log(rotaZ);
            }
        }
        else
        {
            apertou = false;
        }
        transform.Rotate(0,0,speed * Time.deltaTime * dir);
    }
}
