using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextDestroyer : MonoBehaviour
{
    [SerializeField] private float _textDuration; // temps d'apparition du pop up
    private float _timer = 0f;

    // Update is called once per frame
    void Update()
    {
        waitToDestroyText();
    }

    public void waitToDestroyText()
    {
        _timer += Time.deltaTime;
        if (_timer >= _textDuration)
            Destroy(gameObject);
    }
}
