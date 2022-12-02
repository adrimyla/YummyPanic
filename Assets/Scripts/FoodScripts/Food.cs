using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    /// <summary>
    /// Goût de l'ingrédient. Chaque glouton a un goût favori.
    /// Dans l'ordre :
    /// - SWEET : sucré (en rose);
    /// - SPICY : épicé (en rouge) ;
    /// - SOUR : acide (en jaune) ;
    /// - BITTER : amer (en vert) ;
    /// - LUMPY : âpre (en bleu)
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
