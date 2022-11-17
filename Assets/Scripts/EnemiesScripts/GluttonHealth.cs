using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonHealth : MonoBehaviour
{
    /*
     * Attributs
     */
    [SerializeField] private int _maxHP; // points de vie maximum
    [SerializeField] private int _currentHP; // points de vie actuels
    private bool _isAlive = true; // statut du Glouton

    /*
     * Attributs
     */

    // Initialisation
    void Start()
    {
        this._currentHP = this._maxHP;
    }

    // Perte de pv 
    public void TakeDamage(int damageTaken)
    {
        this._currentHP -= damageTaken;
        Debug.Log("PV restants : " + this._currentHP);
        CheckGluttonState();
        if (!this._isAlive)
            KillGlutton();
    }

    // Soin des PV
    public void GetHealed(int healingPoint)
    {
        this._currentHP += healingPoint;
    }

    // Vérification de l'état du Glouton
    private void CheckGluttonState()
    {
        if(this._currentHP <= 0 && this._isAlive)
            this._isAlive = false;
    }

    // Mort du Glouton
    private void KillGlutton()
    {
        Destroy(gameObject);
    }
}
