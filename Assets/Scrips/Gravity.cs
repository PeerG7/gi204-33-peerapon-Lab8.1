using NUnit.Framework;
using System.Collections.Generic; //For list
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;
    Rigidbody rb;

    const float G = 0.00667f;

    public static List<Gravity> gravityobjectlist;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (gravityobjectlist == null )
        {
            gravityobjectlist = new List<Gravity>();
        }
        gravityobjectlist.Add(this);

        //orbit
        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }

    void attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;

        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;

        float ForceMagnitude = G * (rb.mass * otherRb.mass / Mathf.Pow ( distance , 2));
        Vector3 FinalForce = ForceMagnitude * direction.normalized;

        otherRb.AddForce (FinalForce);
    }

    private void FixedUpdate()
    {
        foreach (var obj in gravityobjectlist)
        {
            //call attract
            if (obj != this)
            attract(obj);
        }
    }
}
