using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_handler : MonoBehaviour
{
    public static UI_handler instance;
    public HealthHandler bossHealth;
    public Transform zabite_jednostki; 
    public TextMeshProUGUI text_zabite;
    public TextMeshProUGUI text_nr_fali;
    public Transform opcje;
    public Transform wynik;
    public TextMeshProUGUI text_zabite_wynik;
    public TextMeshProUGUI text_nr_fali_wynik;
    public Button wyjscie;
    public Transform HP_gracza;
    public Transform HP_bossa;
    public Transform Opcje_wyjscie;
    private bool flaga;
    private void Awake()
    {
        HP_bossa.gameObject.SetActive(false);
        wynik.gameObject.SetActive(false);
        opcje.gameObject
            .SetActive(false);
        flaga = false;
        instance = this;
    }
    public void AktualizujIloscZabitych(int ilosc)
    {
        text_zabite.SetText(ilosc.ToString());
    }
    public void AktualizujNRFali(int ilosc)
    {
        text_nr_fali.SetText("Fala:"+ilosc.ToString());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !flaga)
        {

            HP_gracza.gameObject.SetActive(false);
            Time.timeScale = 0;
            opcje.gameObject.SetActive(true);
            flaga = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && flaga)
        {
            HP_gracza.gameObject.SetActive(true);
            flaga = false;
            Time.timeScale = 1;
            opcje.gameObject.SetActive(false);
        }
        bossHealth.KoniecGry += Instance_KoniecGry;
        invaders.instance.Boss += Instance_Boss;
        Opcje_wyjscie.GetComponent<Button>().onClick.AddListener(() =>
        Application.Quit());
    }

    private void Instance_Boss(object sender, System.EventArgs e)
    {
        HP_bossa.gameObject.SetActive(true);
    }

    private void Instance_KoniecGry(object sender, System.EventArgs e)
    {
        HP_bossa.gameObject.SetActive(false);
        HP_gracza.gameObject.SetActive(false);
        wynik.gameObject.SetActive(true) ;
        Time.timeScale = 0;
        text_zabite_wynik.SetText("Zabite jednostki:"+ invaders.instance.GetKilled().ToString());
        text_nr_fali_wynik.SetText("ilosc fal:" + invaders.instance.getNrFali().ToString());
        wyjscie.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            wynik.gameObject.SetActive(false);
            Application.Quit();
        });
    }

}
