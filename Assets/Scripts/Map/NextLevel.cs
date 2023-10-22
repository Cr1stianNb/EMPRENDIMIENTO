using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string nameScene;
    void Start()
    {
        SceneManager.LoadScene(nameScene, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
