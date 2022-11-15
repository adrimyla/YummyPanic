using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*
     * Attributs
     */
    [Header("Vitesses")]
    [SerializeField] private float _walkingSpeed; // vitesse de marche
    [SerializeField] private float _runningSpeed; // vitesse de course
    [SerializeField] private float slowingCoefficient = 0.7f; // Coefficient de ralentissement des mouvements diagonaux
    /*
     * Méthode
     */

    void Update()
    {
        Move();
    }

    /*
     * Mouvements du joueur
     */
    void Move()
    {
        // Calcul de la distance à parcourir
        float verticalDistance = Input.GetAxis("Vertical") * _walkingSpeed * Time.deltaTime;
        float horizontalDistance = Input.GetAxis("Horizontal") * _walkingSpeed * Time.deltaTime;
        /* On "ralentit" légèrement les mouvements en diagonale
        *  Sans cela, le joueur bouge beaucoup plus rapidement que lors de mouvements purement horizontaux ou verticaux.
        */
        if(verticalDistance != 0 && horizontalDistance != 0)
            transform.Translate(horizontalDistance * slowingCoefficient, verticalDistance * slowingCoefficient, 0);
        // Translation de la position du joueur
        transform.Translate(horizontalDistance, verticalDistance, 0);
    }

    /*
     * Etats du joueur
     */
    enum MoveState
    {
        WALKING,
        RUNNING,
    }
}
