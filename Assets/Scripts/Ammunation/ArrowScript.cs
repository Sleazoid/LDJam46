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
    [SerializeField]
    private LayerMask watcherLayer;
    [SerializeField]
    private LayerMask hawkLayer;
    private bool hitSomething = false;
    [SerializeField]
    private GameObject sparklePrefab;
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
        if(hitSomething)
        {
           // Debug.Log(rb.velocity);
            if(rb.velocity.magnitude.Equals(0))
            {
              
                Destroy(this.gameObject);
            }
        }
        else if(transform.position.y<-50)
        {
          
            Destroy(this.gameObject);
        }
       
    }

    public void SetPath(Vector3[] points, float force, bool facingRight)
    {
        shootForce = force;
        linePoints = points;
        move = true;
        float f = force / 14;
        if (facingRight)
        {
            rb.AddForce(this.transform.right * f, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(-this.transform.right * f, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitSomething = true;
        if (collision.gameObject.tag.Equals("Watcher"))
        {
            rb.velocity = new Vector2(0, 0);
            collision.gameObject.GetComponent<WatcherEnemy>().ArrowHit();
            GameObject sp = Instantiate(sparklePrefab, collision.contacts[0].point, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag.Equals("Hawk"))
        {
            rb.velocity = new Vector2(0, 0);
            collision.gameObject.GetComponent<StormBirdEnemy>().ArrowHit();
            GameObject sp = Instantiate(sparklePrefab, collision.contacts[0].point, Quaternion.identity);
            Destroy(this.gameObject);
        }
        //if(collision.gameObject.tag.eq)
    }
}
