using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card :ScriptableObject {

    //Nombre de la carta
    public new string name;
    //Flag de uso
    public bool Used;
    //Acción
    public CardActionEnum.Action Action;
    //probability
    public float probability;

}
