using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomPhraseSelector : MonoBehaviour
{
    [SerializeField] private Image phraseImage;
    [SerializeField] private List<Sprite> phraseList;

    void Start()
    {
        phraseImage.sprite = phraseList[Random.Range(0, phraseList.Count)];
        phraseImage.SetNativeSize();

        RectTransform rt = phraseImage.GetComponent<RectTransform>();
        
        if (rt.rect.width > 350)
        {
            phraseImage.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        }
        else if (rt.rect.width > 200)
        {
            phraseImage.transform.localScale = new Vector3(5f, 5f, 5f);
        }
        else
        {
            phraseImage.transform.localScale = new Vector3(6f, 6f, 6f);
        }
    }
}