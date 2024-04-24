using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Title()
    {
        SceneManager.LoadScene("stage1", LoadSceneMode.Single);
    }
    public void Back()
    {
        SceneManager.LoadScene("stage2", LoadSceneMode.Single);
    }
}
