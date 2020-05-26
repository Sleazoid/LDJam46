using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    [SerializeField]
    private Vector2 centerOfMassVal;
    public bool Awake;
    protected Rigidbody2D r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        r.centerOfMass = centerOfMassVal;
        r.WakeUp();
        Awake = !r.IsSleeping();
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * centerOfMassVal, 0.01f);
    }
}
