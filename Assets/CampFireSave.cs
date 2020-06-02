using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireSave : MonoBehaviour
{
    [SerializeField]
    private GameObject emptyFire;
    [SerializeField]
    private GameObject fullFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("SAVE");
            GameManager.Instance.SavePointReached();
        }
    }
    public void SetFireOn()
    {
        fullFire.SetActive(true);
        emptyFire.SetActive(false);
    }
}
