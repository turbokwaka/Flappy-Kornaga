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
    }
}
