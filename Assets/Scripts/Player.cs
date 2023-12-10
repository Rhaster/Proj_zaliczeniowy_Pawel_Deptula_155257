using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Projectile laserPrefab;
    public float speed = 5;
    private bool _laserActive;
    private SystemHP systemHP;
    private void Awake()
    {
        systemHP = GetComponent<SystemHP>();
        _laserActive = false;
    }
    private void Start()
    {
        systemHP.OnDied += SystemHP_OnDied;
    }

    private void SystemHP_OnDied(object sender, System.EventArgs e)
    {
        SceneManager.LoadScene("SpaceInvaders");
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            if (transform.position.x > -13)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
        }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) ){
            if (transform.position.x < 13)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
       else if( Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }


    }
    private void Shoot()
    {
        if (!_laserActive)
        {
            Projectile proj = Instantiate(laserPrefab, transform.position + Vector3.up, Quaternion.Euler(0,0,90f));
            proj.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }
    private void LaserDestroyed()
    {
        _laserActive= false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.layer == LayerMask.NameToLayer("Invader"))||
                 (collision.gameObject.layer == LayerMask.NameToLayer("MIssle")))
        {
            systemHP.Damage(20);
        }
    }
}
