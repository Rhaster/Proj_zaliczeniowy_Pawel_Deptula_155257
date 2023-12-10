using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HealthHandler : MonoBehaviour
{
    public Image HealthBar;
    public SystemHP sysHP;
    public event EventHandler KoniecGry;
    public static HealthHandler Instance;
    private void Awake()
    {
        Instance= this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        sysHP = GetComponent<SystemHP>();
        HealthBar.fillAmount = sysHP.GetHealthAmountNormalized();
        sysHP.OnDamaged += SysHP_OnDamaged;
        sysHP.OnDied += SysHP_OnDied;
    }

    private void SysHP_OnDied(object sender, System.EventArgs e)
    {
        KoniecGry?.Invoke(this,EventArgs.Empty);
    }
    
    private void SysHP_OnDamaged(object sender, System.EventArgs e)
    {
        HealthBar.fillAmount = sysHP.GetHealthAmountNormalized();
    }

  


}
