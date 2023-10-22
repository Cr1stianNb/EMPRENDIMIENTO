using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject bat;
    public GameObject slime;
    void Start()
    {
        Instantiate(bat, new Vector3(-2.731094f, -2.614105f, 0), Quaternion.identity);
        Instantiate(bat, new Vector3(-3.175411f, 0.0532935f, 0), Quaternion.identity);
        Instantiate(slime, new Vector3(3.267812f, 1.840097f, 0), Quaternion.identity);
    }
}
