using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplay : MonoBehaviour {

    [HideInInspector]public Card Card;

    //Sprite de la carta
    public Animator NewAnimator;
    //Nombre de la carta
    public new string name;
    //Flag de uso
    public bool Used;
    //Acción
    public CardActionEnum.Action Action;
    [SerializeField] private CardActivated cardActivated;
    private GameManager gm;

    void Start ()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cardActivated = GetComponent<CardActivated>();
        Card = cardActivated.ChooseCard();
       // gm.StartCoroutine(gm.FirstReRollCard(cardActivated));
        UpdateCardAtributes();

    }

    private void UpdateCardAtributes()
    {
        
        //Cambiar animaciones
        Animator animator = GetComponent<UnityEngine.Animator>();
        animator.runtimeAnimatorController = Card.Animator;
      

        name = Card.name;
        Used = Card.Used;
        Action = Card.Action;
    }

    public void UpdateCard(Card NewCard)
    {
        Card = NewCard;
        UpdateCardAtributes();
    }

  
}
