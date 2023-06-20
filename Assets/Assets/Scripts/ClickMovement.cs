using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    public bool playerIsWalking;
    public Transform player;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();   
    }
    public void GoToItem(ItemData item)
    {
        //Update hint box
        gameManager.UpdateHintBox(null, false);
        playerIsWalking = true;
        //start moving player
        StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position));
        //equipment stuff
        TryGettingItem(item);
    }
    private void TryGettingItem(ItemData item)
    {
        bool canGetItem = item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID);
        if (canGetItem)
        {
            GameManager.collectedItems.Add(item.ItemID);
        }
        StartCoroutine(UpdateSceneAfterAction(item, canGetItem));
    }
    private IEnumerator UpdateSceneAfterAction(ItemData item, bool canGetItem)
    {
        while (playerIsWalking)
            yield return new WaitForSeconds(0.05f);
        if (canGetItem)
        {
            foreach (GameObject g in item.objectsToRemove)
                Destroy(g);
            Debug.Log("ItemCollected");
        }
        else
            gameManager.UpdateHintBox(item, player.GetComponentInChildren<SpriteRenderer>().flipX);
        yield return null;
    }
}
