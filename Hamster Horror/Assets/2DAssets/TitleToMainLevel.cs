using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleToMainLevel : MonoBehaviour
{

    public string Level1Scene; 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchtogame()
    {
      SceneManager.LoadScene(Level1Scene);
    }
}
