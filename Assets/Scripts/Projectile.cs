using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public System.Action destroyed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != LayerMask.NameToLayer("MIssle"))
                {
            if (destroyed != null)
            {
                Destroy(this.gameObject);
                this.destroyed.Invoke();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}