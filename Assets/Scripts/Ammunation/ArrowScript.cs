using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    private Vector3[] linePoints;// = new List<Vector2>();
    private Rigidbody2D rb;
    int pointIndex = 0;
    float shootForce;
    bool move = false;
    float angle;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.gravityScale = 
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Vector2 v = rb.velocity;
            angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //    for (int i = 0; i < linePoints.Length - 1; i++)
            //    {
            //        this.transform.LookAt(linePoints[i + 1]);
            //        this.transform.Translate(Vector2.right * shootForce/1000 * Time.deltaTime);
            //    }
        }

    }

    public void SetPath(Vector3[] points, float force, bool facingRight)
    {
        shootForce = force;
        linePoints = points;
        move = true;
        float f = force / 14;
        if(facingRight)
        {
            rb.AddForce(this.transform.right * f, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(-this.transform.right * f, ForceMode2D.Impulse);
        }
    }
}
