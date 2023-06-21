using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();
    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;

    [Header("Setup")]
    public RectTransform nameTag, hintBox;
    public TextManager dialogue;

    [Header("Local Scenes")]
    public Image blockingImage;
    public GameObject[] localScenes;
    int activeLocalScene = 0;
    public Transform[] playerStartPosition;

    [Header("Start Scene")]
    public GameObject startSceneCanvas;
    public GameObject intro;
    public GameObject Text;

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
    public void CheckSpecialConditions(ItemData item, bool canGetItem)
    {
        switch (item.ItemID)
        {
            case -11:
                StartCoroutine(ChangeScene(2, 0));
                break;
            case -12:
                StartCoroutine(ChangeScene(1, 0));
                break;
            case -13:
                StartCoroutine(ChangeScene(3, 0));
                break;
            case -14:
                StartCoroutine(ChangeScene(4, 0));
                break;
            case -1:
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

        if(sceneNumber == 5)
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
            blockingImage.color = c;
        }
        //esconde la escena anterior
        localScenes[activeLocalScene].SetActive(false);
        //muestra la escena esperada
        localScenes[sceneNumber].SetActive(true);
        //en que escena estoy
        activeLocalScene = sceneNumber;
        //ubica al jugador
        FindObjectOfType<ClickMovement>().player.position = playerStartPosition[sceneNumber].position;

        UpdateHintBox(null, false);
        UpdateNameTag(null);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        startSceneCanvas.SetActive(false);
        intro.SetActive(true);
        dialogue.StartDialogue();
        StartCoroutine(ChangeScene(1, 12.5f));
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
