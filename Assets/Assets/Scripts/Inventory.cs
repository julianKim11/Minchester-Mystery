using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool inventoryEnabled;
    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot; 

    public GameObject inventory;
    public GameObject slotHolder;
    public GameObject inventoryE;
    public GameObject slotHolderE;

    public float pickupRange = 5f;

    private void Start()
    {
        allSlots = slotHolder.transform.childCount;

        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if(slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0)) // Botón izquierdo del mouse
        //{
        //    Debug.Log("CLICK1");
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Debug.Log("CLICK2");
        //        GameObject clickedObject = hit.collider.gameObject;
        //        float distanceToClickedObject = Vector3.Distance(transform.position, clickedObject.transform.position);

        //        if (distanceToClickedObject <= pickupRange)
        //        {
        //            Debug.Log("CLICK3");
        //            Item item = clickedObject.GetComponent<Item>();
        //            if (item != null)
        //            {
        //                Debug.Log("add");
        //                AddItem(clickedObject, item.ID, item.type, item.description, item.icon);
        //            }
        //        }
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }

        if(inventoryEnabled == true)
        {
            inventory.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Debug.Log("COLISION");
            GameObject itemPickedUp = collision.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            AddItem(itemPickedUp, item.ID, item.type, item.description, item.icon);
        }
    }

    public void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;
                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().description = itemDescription;
                slot[i].GetComponent<Slot>().icon = itemIcon;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();

                slot[i].GetComponent<Slot>().empty = false;
                break;
            }
            
        }
    }
    public bool HasItem(string itemType)
    {
        foreach (GameObject slot in slot)
        {
            Slot slotComponent = slot.GetComponent<Slot>();
            if(!slotComponent.empty && slotComponent.type == itemType)
            {
                return true;
            }
        }
        return false;
    }
}
