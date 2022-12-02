using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    /// <summary>
    /// Go�t de l'ingr�dient. Chaque glouton a un go�t favori.
    /// Dans l'ordre :
    /// - SWEET : sucr� (en rose);
    /// - SPICY : �pic� (en rouge) ;
    /// - SOUR : acide (en jaune) ;
    /// - BITTER : amer (en vert) ;
    /// - LUMPY : �pre (en bleu)
    /// </summary>
    public enum Taste
    {
        SWEET,
        SPICY,
        SOUR,
        BITTER,
        LUMPY
    }
}
