using UnityEngine;
using TMPro;
using System.Collections;

public class TextManager : MonoBehaviour
{
    public TMP_Text[] texts;
    private int currentIndex = 0;

    public void StartDialogue()
    {
        StartCoroutine(ShowTexts());
    }
    private IEnumerator ShowTexts()
    {
        while (currentIndex < texts.Length)
        {
            texts[currentIndex].gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            texts[currentIndex].gameObject.SetActive(false);
            currentIndex++;
        }
    }
}