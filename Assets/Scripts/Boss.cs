using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class Boss : MonoBehaviour
{
    private bool flaga;
    public float timer;
    public float timermax;
    public float timer1;
    public float timermax1;
    public SystemHP sysHP;
    public Projectile MisslePrefab;
    public Projectile MisslePrefab1;
    public Transform missilespawnpoint;
    private Vector3 kierunekRuchu = new Vector3(1, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        timermax = 1; // strzal co sek 
        timermax1 = 1.5f;
        
        invaders.instance.Boss += Instance_Boss;
        gameObject.SetActive(false);

    }

    private void Instance_Boss(object sender, System.EventArgs e)
    {

        flaga= true;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(flaga)
        {
            timer += Time.deltaTime;
            if (timer > timermax)
            {
                MisslePrefab.direction.x = Random.Range(-1f, 1+0.1f);
                timer = 0;
                Vector3 rotationEulerAngles = new Vector3(0, 0,  GetAngleFromVector(MisslePrefab.direction));
                Instantiate(MisslePrefab, missilespawnpoint.position, Quaternion.Euler(rotationEulerAngles));
            }
            timer1 += Time.deltaTime;
            if (timer1 > timermax1)
            {

                timer1 = 0;
                Instantiate(this.MisslePrefab1, missilespawnpoint.position, UnityEngine.Quaternion.Euler(0, 0, -90f));
            }

            transform.Translate(kierunekRuchu * 2 * Time.deltaTime);

            if (transform.position.x < -3 || transform.position.x > 3)
            {
                kierunekRuchu *= -1;
            }
        }
        
    }
    public static float GetAngleFromVector(UnityEngine.Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sysHP.Damage(10);
    }
}
