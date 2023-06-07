using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField, Range(0, 10)]
    int power = 5;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void RandomAddForce()
    {
        Vector3 rndVec = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f));
        rb.AddForce(rndVec * power, ForceMode.Impulse);
    }
}