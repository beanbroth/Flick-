using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float _degreeRotationsPerSecond;
    void Update()
    {
        transform.Rotate(0, _degreeRotationsPerSecond * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
            Debug.Log("Picked up coni");
        }
    }
}
