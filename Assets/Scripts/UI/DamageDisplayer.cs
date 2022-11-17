using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class DamageDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _damageText;
    [SerializeField] private PlayerAttack _playerAttack;

    private void Update()
    {
        
    }

    public void ShowDamage(int damageDealt)
    {
        if(_playerAttack.target != null)
        {
            GameObject target = _playerAttack.target;
            Transform targetTransform = target.transform;
            Vector2 textPosition = new Vector2(targetTransform.position.x, targetTransform.position.y - targetTransform.localScale.y / 2 + 1.5f);
            GameObject damageTextPopUp = Instantiate(_damageText, textPosition, Quaternion.identity);
            damageTextPopUp.GetComponent<TextMeshPro>().text = damageDealt.ToString();
        }
    }

    
}
