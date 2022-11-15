using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /*
     * Attributs
     */
    [SerializeField] private int _damageDealt; // dégâts infligé
    private int _damageBonus = 1; // bonus de dégats lorsque le joueur obtient un buff. 

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Ennemi détecté");
            if (Input.GetKey(KeyCode.E))
            {
                other.gameObject.GetComponent<GluttonHealth>().TakeDamage(this._damageDealt * this._damageBonus);
                Debug.Log("J'attaque");
            }
        }
    }
}
