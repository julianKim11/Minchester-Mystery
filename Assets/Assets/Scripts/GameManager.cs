using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static List<ItemData> collectedItems = new List<ItemData>();
    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    public AnimationData[] playerAnimations;

    [Header("Setup")]
    public RectTransform nameTag, hintBox;
    public TextManager dialogue;
    public AudioSource soundtrackSource;

    [Header("Local Scenes")]
    public Image blockingImage;
    public GameObject[] localScenes;
    int activeLocalScene = 1;
    public Transform[] playerStartPosition;

    [Header("Start Scene")]
    public GameObject startSceneCanvas;
    public GameObject intro;
    public GameObject MainMenu;

    [Header("Equipment")]
    public GameObject equipmentCanvas;
    public Image[] equipmentSlots, equipmentImage;
    public Sprite emptyItemSlotSprite;
    public Color selectedItemColor;
    public int selectedCanvasSlotID = 0;
    public ItemData.items selectedItemID = ItemData.items.none;

    private void Start()
    {
        
    }
    private void Update()
    {
        Debug.Log(collectedItems[1]);
        Debug.Log(collectedItems[2]);
        Debug.Log(collectedItems[3]);
        Debug.Log(collectedItems[4]);
    }
    public IEnumerator MoveToPoint(Transform myObject, Vector2 point)
    {
        Vector2 positionDifference = point - (Vector2)myObject.position;

        if (myObject.GetComponentInChildren<SpriteRenderer>() && positionDifference.x != 0)
            myObject.GetComponentInChildren<SpriteRenderer>().flipX = positionDifference.x > 0;

        while (positionDifference.magnitude > moveAccuracy)
        {
            myObject.Translate(moveSpeed * positionDifference.normalized * Time.deltaTime);
            positionDifference = point - (Vector2)myObject.position;
            yield return null;
        }
        myObject.position = point;

        if (myObject == FindObjectOfType<ClickMovement>().player)
            FindObjectOfType<ClickMovement>().playerIsWalking = false;
        yield return null;
    }

    public void UpdateNameTag(ItemData item)
    {
        if (item == null)
        {
            nameTag.parent.gameObject.SetActive(false);
            return;
        }
        nameTag.parent.gameObject.SetActive(true);
        string nameText = item.objectName;
        Vector2 size = item.nameTagSize;

        if (collectedItems.Contains(item))
        {
            nameText = item.itemName;
            size = item.itemNameTagSize;
        }
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = nameText;
        nameTag.sizeDelta = size;
        nameTag.localPosition = new Vector2(-item.nameTagSize.x*2.5f, -5f);
    }
    public void UpdateHintBox(ItemData item, bool playerFlipped)
    {
        if (item == null)
        {
            Debug.Log("ShowHintBox1");
            hintBox.gameObject.SetActive(false);
            return;
        }
        //show hint box
        Debug.Log("ShowHintBox2");
        hintBox.gameObject.SetActive(true);
        //change name
        hintBox.GetComponentInChildren<TextMeshProUGUI>().text = item.hintMessage;
        //change size
        hintBox.sizeDelta = item.hintBoxSize;
        //move hint box
        if (playerFlipped)
            hintBox.parent.localPosition = new Vector2(0,  0);
        else
            hintBox.parent.localPosition = Vector2.zero;
    }
    public void CheckSpecialConditions(ItemData item, bool canGetItem)
    {
        switch (item.ItemID)
        {
            case ItemData.items.goToScene2:
                StartCoroutine(ChangeScene(2, 0));
                break;
            case ItemData.items.goToScene1:
                StartCoroutine(ChangeScene(1, 0));
                break;
            case ItemData.items.goToScene3:
                StartCoroutine(ChangeScene(3, 0));
                break;
            case ItemData.items.goToScene4:
                StartCoroutine(ChangeScene(4, 0));
                break;
            case ItemData.items.goToScene5:
                if (canGetItem)
                {
                    StartCoroutine(ChangeScene(5, 1));
                }
                break;
        }

    }
    public IEnumerator ChangeScene(int sceneNumber, float delay)
    {
        yield return new WaitForSeconds(delay);
        intro.SetActive(false);
        if (sceneNumber == localScenes.Length - 1)
        {
            FindObjectOfType<ClickMovement>().player.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        Color c = blockingImage.color;
        //pantalla se pone negro durante 1 segundo y el click bloqeado
        blockingImage.enabled = true;
        while (blockingImage.color.a < 1)
        {
            //aumentar alpha del color
            c.a += Time.deltaTime;
            //yield return null;
            blockingImage.color = c;
        }
        //esconde la escena anterior
        localScenes[activeLocalScene].SetActive(false);
        //muestra la escena esperada
        localScenes[sceneNumber].SetActive(true);

        if (activeLocalScene == 0)
        {
            soundtrackSource.Play();
        }
            
        //en que escena estoy
        activeLocalScene = sceneNumber;

        //ubica al jugador
        FindObjectOfType<ClickMovement>().player.position = playerStartPosition[sceneNumber].position;

        UpdateHintBox(null, false);
        UpdateNameTag(null);

        equipmentCanvas.SetActive(sceneNumber > 0 && sceneNumber <localScenes.Length-1);

        while (blockingImage.color.a > 0)
        {
            //bajar alpha del color
            c.a -= Time.deltaTime;
            blockingImage.color = c;
        }
        blockingImage.enabled = false;
        yield return null;
    }
    public void RestartGame()
    {
        collectedItems = new List<ItemData>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void GoToMenu()
    {
        collectedItems = new List<ItemData>();
        StartCoroutine(ChangeScene(0, 1));
    }
    public void StartGame()
    {
        startSceneCanvas.SetActive(false);
        intro.SetActive(true);
        dialogue.StartDialogue();
        StartCoroutine(ChangeScene(1, 10.5f));
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SelectItem(int equipmentCanvasID)
    {
        Color c = Color.white;
        c.a = 0;
        //change the alpha of the previous slot to 0
        equipmentSlots[selectedCanvasSlotID].color = c;

        //save changes and stop if an empty slot is clicked or the last item is removed
        if (equipmentCanvasID >= collectedItems.Count || equipmentCanvasID < 0)
        {
            //no items selected
            selectedItemID = ItemData.items.none;
            selectedCanvasSlotID = 0;
            return;
        }

        //change the alpha of the new slot to x
        equipmentSlots[equipmentCanvasID].color = selectedItemColor;
        //save changes
        selectedCanvasSlotID = equipmentCanvasID;
        selectedItemID = collectedItems[selectedCanvasSlotID].ItemID;
        //if (collectedItems[selectedCanvasSlotID] != null)
        //{
        //    selectedItemID = collectedItems[selectedCanvasSlotID].ItemID;
        //}
        //Color c = Color.white;
        //c.a = 0;

        //equipmentSlots[selectedCanvasSlotID].GetComponent<Image>().color = c;

        //if(equipmentCanvasID >= collectedItems.Count || equipmentCanvasID < 0)
        //{
        //    selectedItemID = ItemData.items.none;
        //    selectedCanvasSlotID = 0;
        //    return;
        //}

        //equipmentSlots[equipmentCanvasID].GetComponent<Image>().color = selectedItemColor;
        //selectedCanvasSlotID = equipmentCanvasID;
        //selectedItemID = collectedItems[selectedCanvasSlotID].ItemID;

    }
    public void ShowItemName(int equipmentCanvasID)
    {
        if(equipmentCanvasID < collectedItems.Count)
        {
            UpdateNameTag(collectedItems[equipmentCanvasID]);
        }
    }
    public void UpdateEquipmentCanvas()
    {
        //find out how many items we have and when to stop
        int itemsAmount = collectedItems.Count, itemSlotsAmount = equipmentSlots.Length;
        //replace no item sprites and old sprites with collectedItems[x].itemSlotSprite
        for (int i = 0; i < itemSlotsAmount; i++)
        {
            //choose between emptyItemSlotSprite and an item sprite
            if (i < itemsAmount && collectedItems[i].itemSlotSprite != null)
                equipmentImage[i].sprite = collectedItems[i].itemSlotSprite;
            else
                equipmentImage[i].sprite = emptyItemSlotSprite;
        }
        //add special conditions for selecting items
        if (itemsAmount == 0)
            SelectItem(-1);
        else if (itemsAmount == 1)
            SelectItem(0);
        //int itemAmount = collectedItems.Count, itemSlotsAmount = equipmentSlots.Length;

        //for(int i = 0; i < itemSlotsAmount; i++)
        //{
        //    if (i < itemAmount && collectedItems[i].itemSlotSprite != null)
        //    {
        //        equipmentImage[i].sprite = collectedItems[i].itemSlotSprite;
        //    }
        //    //else
        //    //{
        //    //    equipmentImage[i].sprite = emptyItemSlotSprite;
        //    //}
        //}
        //if (itemAmount == 0)
        //{
        //    SelectItem(-1);
        //}else if(itemAmount == 1)
        //{
        //    SelectItem(0);
        //}
    }
}
