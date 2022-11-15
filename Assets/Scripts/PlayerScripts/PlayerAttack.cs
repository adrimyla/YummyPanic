using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /*
     * Attributs
     */
    [SerializeField] private int _damageDealt; // d�g�ts inflig�
    private int _damageBonus = 1; // bonus de d�gats lorsque le joueur obtient un buff. 

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Ennemi d�tect�");
            if (Input.GetKey(KeyCode.E))
            {
                other.gameObject.GetComponent<GluttonHealth>().TakeDamage(this._damageDealt * this._damageBonus);
                Debug.Log("J'attaque");
            }
        }
    }
}
