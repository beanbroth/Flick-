using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.PlayerRef.transform.position = transform.position;
        GameManager.Instance.PlayerRef.GetComponent<Player>().SetVelToZero();
    }

}
