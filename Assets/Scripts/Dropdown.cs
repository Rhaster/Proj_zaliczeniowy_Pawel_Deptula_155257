using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Dropdowna : MonoBehaviour
{

    public TMP_Dropdown waveDropdown;

    private void Start()
    {
        waveDropdown = GetComponent<TMP_Dropdown>();
        TMP_Dropdown.OptionDataList options = new TMP_Dropdown.OptionDataList();
        for (int i = 1; i <= 10; i++)
        {
            options.options.Add(new TMP_Dropdown.OptionData(i.ToString()));
        }
        waveDropdown.options = options.options;

        // Ustaw domy�ln� warto�� Dropdown na 1
        waveDropdown.value = 0;

        // Dodaj metod� OnWaveValueChanged do obs�ugi zdarzenia zmiany warto�ci w Dropdown
        waveDropdown.onValueChanged.AddListener(OnWaveValueChanged);
    }

    // Metoda wywo�ywana, gdy u�ytkownik zmieni warto�� w Dropdown
    private void OnWaveValueChanged(int value)
    {
        // Pobierz aktualnie wybran� warto��
        int selectedWave = value + 1;

        // Zapisz wybran� warto�� do PlayerPrefs pod kluczem "fale"
        PlayerPrefs.SetInt("fale", selectedWave);
        PlayerPrefs.Save();
    }
}

