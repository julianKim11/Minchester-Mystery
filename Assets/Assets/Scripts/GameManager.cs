using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();
    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    public RectTransform nameTag, hintBox;
    
    public IEnumerator MoveToPoint(Transform myObject, Vector2 point)
    {
        Vector2 positionDifference = point - (Vector2)myObject.position;
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
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = item.objectName;
        nameTag.sizeDelta = item.nameTagSize;
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
    public void CheckSpecialConditions(ItemData item)
    {
        switch (item.ItemID)
        {
            case -11:
                StartCoroutine(ChangeScene(1, 0));
                break;
            case -12:
                StartCoroutine(ChangeScene(2, 0));
                break;
            case -1:
                StartCoroutine(ChangeScene(3, 1));
                break;
        }

    }
    public IEnumerator ChangeScene(int sceneNumber, float delay)
    {
        yield return null;
    }
}
