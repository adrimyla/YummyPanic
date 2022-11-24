using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /*
     * Attributs
     */
    [Header("Statistiques")]
    [SerializeField] private int _damageDealt; // d�g�ts inflig�
    [SerializeField] private float _attackDelay = 0.25f; // d�lais entre les d�g�ts
    [HideInInspector] public GameObject target; // cible du joueur. A modifier plus tard pour prendre en compte les attaques en AOE
    private bool _canAttack = true; // �tat d'offensive ou pas de la part du joueur
    private int _damageBonus = 1; // bonus de d�gats lorsque le joueur obtient un buff. 
    private float _timerBeforeNextAttack = 0f; // timer �valuant la dur�e �coul�e apr�s une attaque

    [SerializeField] private DamageDisplayer _damageDisplayer;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && target != null && _canAttack)
            AttackTarget();
        if(!_canAttack)
        {
            _timerBeforeNextAttack += Time.deltaTime;
            if(_timerBeforeNextAttack >= _attackDelay)
            {
                _timerBeforeNextAttack = 0f;
                _canAttack = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && target != null)
        {
            target = null;
        }
    }

    void AttackTarget()
    {
        target.GetComponent<GluttonHealth>().TakeDamage(this._damageDealt);
        // Affichages
        _damageDisplayer.ShowDamage(_damageDealt);
        //_damageDisplayer.SetDamageColor(target.GetComponent<SpriteRenderer>());
        Debug.Log("J'attaque");
        _canAttack = false;
    }
}
