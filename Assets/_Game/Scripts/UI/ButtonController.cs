using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject btnThrowSword;
    private bool changePhase;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();

        btnThrowSword.SetActive(false);
        changePhase = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePhase()
    {
        changePhase = !changePhase;

        if (changePhase)
        {
            btnThrowSword.SetActive(true);
        }
        else
        {
            btnThrowSword.SetActive(false);
        }
    }


}
