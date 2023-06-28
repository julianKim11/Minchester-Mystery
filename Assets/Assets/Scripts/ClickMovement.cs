using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    [Header("General")]
    public bool playerIsWalking = true;
    public Transform player;
    GameManager gameManager;
    private float maxDistance = 5f;

    [Header("Go To Click")]
    float goToClickMaxY = 1.7f;
    Camera myCamera;
    Coroutine goToClickCoroutine;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    //public void Update()
    //{
    //    if (Input.GetMouseButtonUp(0))
    //        goToClickCoroutine = StartCoroutine(GoToClick(Input.mousePosition));
    //}
    //public IEnumerator GoToClick(Vector2 mousePos)
    //{
    //    //wait to make room for GoToItem() checks
    //    yield return new WaitForSeconds(0.05f);

    //    //Vector2 targetPos = myCamera.ScreenToWorldPoint(mousePos);
    //    //if (targetPos.y > goToClickMaxY || playerIsWalking)
    //    //    yield break;

    //    //hide hint box
    //    gameManager.UpdateHintBox(null, false);
    //    //start walking
    //    playerIsWalking = true;
    //    StartCoroutine(gameManager.MoveToPoint(player, targetPos));
    //    //play animation
    //    player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]);
    //    //stop walking
    //    StartCoroutine(CleanAfterClick());
    //}
    private IEnumerator CleanAfterClick()
    {
        while (playerIsWalking)
            yield return new WaitForSeconds(0.05f);
        player.GetComponent<SpriteAnimator>().PlayAnimation(null);
        yield return null;
    }
    public void GoToItem(ItemData item)
    {
        if (!playerIsWalking)
        {
            gameManager.UpdateHintBox(null, false);
            playerIsWalking = true;
            StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position));

            //StartCoroutine(MovePlayerAndTriggerDialog(item));
            //equipment stuff
            TryGettingItem(item);
        }
    }
    public void TryGettingItem(ItemData item)
    {
        bool canGetItem = item.requiredItemID == ItemData.items.none || gameManager.selectedItemID == item.requiredItemID;

        if (canGetItem)
        {
            GameManager.collectedItems.Add(item);
        }
        StartCoroutine(UpdateSceneAfterAction(item, canGetItem));
    }
    private IEnumerator UpdateSceneAfterAction(ItemData item, bool canGetItem)
    {
        yield return null;
        if (goToClickCoroutine != null)
            StopCoroutine(goToClickCoroutine);

        //wait for player reaching target
        while (playerIsWalking)
            yield return new WaitForSeconds(0.05f);
        while (playerIsWalking)
            yield return new WaitForSeconds(0.05f);

        if (canGetItem)
        {
            foreach (GameObject g in item.objectsToRemove)
                Destroy(g);
            foreach (GameObject g in item.objectsToSetActive)
                g.SetActive(true);
            if (item.successAnimation)
            {
                item.GetComponent<SpriteAnimator>().PlayAnimation(item.successAnimation);
            }
            Debug.Log("ItemCollected");
            gameManager.UpdateEquipmentCanvas();
        }
        else
        {
            gameManager.UpdateHintBox(item, player.GetComponentInChildren<SpriteRenderer>().flipX);
        }

        gameManager.CheckSpecialConditions(item, canGetItem);
        yield return null;
    }
}
