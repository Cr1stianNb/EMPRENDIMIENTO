using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coop : MonoBehaviour
{
    // Start is called before the first frame update
    public bool coop = false;
    public GameObject player2;
    void Start()
    {
        if(coop)
        {
            Instantiate(player2, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

}
