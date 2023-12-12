using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.SceneManagement;
public class invaders : MonoBehaviour
{
    public event EventHandler Boss;
    public Transform spawnpos;
    public static invaders instance;
    public invader[] prefabs;
    public int iloscfal;
    public int ilezab;
    int i;
    public int rows;
    public int cols;
    public AnimationCurve speed;
    public Projectile MisslePrefab;
    private Vector3 Direction = Vector3.right;
    public Transform bossPrefab;
    public int amountKilled {  get; private set; }
    public int totalInvaders;
    public float procentKillsed => (float)this.amountKilled/ (float)this.totalInvaders;
    public float amoundalive => this.totalInvaders - this.amountKilled;

    public float MissleAttackRate = 1;
    private void Awake()
    {
        if(PlayerPrefs.HasKey("fale"))
        {
            iloscfal = PlayerPrefs.GetInt("fale", 1);
            Debug.Log("wczytano ilosc fal = "+ iloscfal);
        }

        instance = this;
        i = 1;
        fala(i);
        przywolanie();
    }
    private void przywolanie()
    {

        for (int row = 0; row < this.rows; row++)
        {
            float width = 2 * (this.cols - 1);
            float height = 2 * (this.rows - 1);
            Vector2 Centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(Centering.x, Centering.y + (row * 2), 0);
            for (int col = 0; col < this.cols; col++)
            {

                invader invaderr = Instantiate(this.prefabs[row%prefabs.Length], this.transform);
                invaderr.killed += InvaderKilled;
                Vector3 pos = rowPosition;
                pos.x += col * 2;
                invaderr.transform.localPosition = pos;
            }
        }
        InvokeRepeating(nameof(MissleAttack), MissleAttackRate, MissleAttackRate);
    }
    private void fala(int numer)
    {
        ilezab = 0;
        rows = 0+ numer;
        cols = 1+ numer;

        if (UnityEngine.Random.value < (0.5))
        {
            totalInvaders = rows * cols;
            InvokeRepeating(nameof(MissleAttack), MissleAttackRate, MissleAttackRate);
            return;
        }
        else
        {
            int holder = rows;
            rows = cols;
            cols= holder;
            totalInvaders = rows * cols;
            InvokeRepeating(nameof(MissleAttack), MissleAttackRate, MissleAttackRate);
            return;
        }
    }

    private void MissleAttack()
    {
        foreach (Transform Invader in this.transform) // iteracja po kazdym child transform tego obiektu 
        {
            if (!Invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (UnityEngine.Random.Range(0,2)  == 1)
            {
                Instantiate(this.MisslePrefab,Invader.position,Quaternion.Euler(0,0,-90f));
                break;
            }
        }
    }
    private void Update()
    {
        this.transform.position += Direction * this.speed.Evaluate(this.procentKillsed) * Time.deltaTime;
        Vector3 leftedge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightedge = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach ( Transform Invader in this.transform) // iteracja po kazdym child transform tego obiektu 
        {
            if(!Invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if((Direction == Vector3.right)&& (Invader.position.x >= (rightedge.x -1)))
            {
                AdvanceRow();
            }else if((Direction == Vector3.left) && (Invader.position.x <= (leftedge.x+1)))
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        Direction.x *= -1;
        Vector3 pos = this.transform.position;
        pos.y -= 1;
        this.transform.position = pos;
    }
    private void InvaderKilled()
    {
        amountKilled++;
        ilezab++;
        UI_handler.instance.AktualizujIloscZabitych(amountKilled);
        totalInvaders = rows * cols;
        if (ilezab  >= totalInvaders)
        {
            i += 1;
            ilezab = 0;
            fala(i);
            UI_handler.instance.AktualizujNRFali(i);
            przywolanie();
            if (i == iloscfal+1)
            {
                Instantiate(bossPrefab, spawnpos.position, Quaternion.identity);
                Boss?.Invoke(this, EventArgs.Empty);
                gameObject.SetActive(false);
            }
        }
    }
    public int getNrFali()
    {
        return i;
    }
    public int GetKilled()
    {
        return amountKilled;
    }
}
