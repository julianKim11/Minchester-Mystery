using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public int ID;
    public string type;
    public string description;
    public Sprite icon;
    public bool pickedUp;
    //public bool equipped;
    [SerializeField] private GameObject panel;
    private bool panelActive = false;
    
    public void ItemUsage()
    {
        if(type == "Nota")
        {
            if (panelActive)
            {
                // Desactivar el panel
                panel.SetActive(false);
                panelActive = false;
            }
            else
            {
                // Activar el panel
                panel.SetActive(true);
                panelActive = true;
            }
        } 
    }
}
