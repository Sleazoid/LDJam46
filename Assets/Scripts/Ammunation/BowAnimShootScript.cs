using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAnimShootScript : MonoBehaviour
{
    [SerializeField]
    private BowAim bowAim;
    // Start is called before the first frame update
    public void SendShootMessage()
    {
   
        bowAim.ShootArrow();
    }
}
