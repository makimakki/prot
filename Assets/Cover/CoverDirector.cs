using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CoverDirector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSearch(){

        SceneManager.LoadScene("search");
    }

    public void ClickTown(){

        SceneManager.LoadScene("demoscene_city_level");
    }

    public void ClickRoom(){

        SceneManager.LoadScene("room");
    }
}
