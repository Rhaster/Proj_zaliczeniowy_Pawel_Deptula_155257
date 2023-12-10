using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuHandler : MonoBehaviour
{
    [SerializeField] private string NameScene;

    public Transform Nowagra_Transform;
    public Transform Opcje_Transform;
    public Transform Wyjscie_Transform;
    public Transform Dzwiek_Transform;
    private void Awake()
    {
        Dzwiek_Transform.gameObject.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        Nowagra_Transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(NameScene);
        });
        Opcje_Transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Opcje_Transform.GetComponent<ButonHandler>().Reset();
            Dzwiek_Transform.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
        Wyjscie_Transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
