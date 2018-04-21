using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card :ScriptableObject {

    //Sprite de la carta
    public Sprite artwork;
    //Nombre de la carta
    public new string name;
    //Flag de uso
    public bool Used;
    //Acción
    public CardActionEnum.Action Action;

}
