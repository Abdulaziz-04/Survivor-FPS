using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Image health_bar, stamina_bar;
    public void displayHealth(float health)
    {
        health_bar.fillAmount = health / 100f;
    }
    public void displayStamina(float stamina)
    {
        stamina_bar.fillAmount = stamina / 100f;

    }

}