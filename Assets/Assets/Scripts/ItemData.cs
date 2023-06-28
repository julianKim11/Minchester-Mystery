using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public enum items
    {
        none,
        goToScene1,
        goToScene2,
        goToScene3,
        goToScene4,
        goToScene5,
        safeKey,
        gardenKey,
        mouse,
        shovel,
        cheese,
        badge,
        evidence,
        laptop,
        unrechable
    }
    [Header("Setup")]
    public items ItemID, requiredItemID;
    public Transform goToPoint;
    public string objectName;
    public Vector2 nameTagSize = new Vector2(3, 0.65f);
    public string itemName;
    public Vector2 itemNameTagSize = new Vector2(3, 0.65f);

    [Header("Success")]
    public GameObject[] objectsToRemove;
    public GameObject[] objectsToSetActive;
    public Sprite itemSlotSprite;
    public AnimationData successAnimation;

    [Header("Failure")]
    [TextArea(3, 3)]
    public string hintMessage;
    public Vector2 hintBoxSize = new Vector2(4, 1.2f);
}
