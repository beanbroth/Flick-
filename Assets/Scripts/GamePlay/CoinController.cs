using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float _degreeRotationsPerSecond;
    bool _hasHit = false;

    void Update()
    {
        transform.Rotate(0, _degreeRotationsPerSecond * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hasHit)
            return;

        if (collision.tag == "Player")
        {
            _hasHit = true;
            CoinSoundController.Play();
            gameObject.SetActive(false);
            Debug.Log("Picked up coni");
        }
    }
}
