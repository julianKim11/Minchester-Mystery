using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [Header("Setup")]
    public int ItemID, requiredItemID;
    public Transform goToPoint;
    public string objectName;
    public Vector2 nameTagSize = new Vector2(3, 0.65f);

    [Header("Success")]
    public GameObject[] objectsToRemove;

    [Header("Failure")]
    [TextArea(3, 3)]
    public string hintMessage;
    public Vector2 hintBoxSize = new Vector2(4, 1.2f);
}
