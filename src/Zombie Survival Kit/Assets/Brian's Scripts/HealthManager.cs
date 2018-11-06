using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// HealthManager: Is a class used to keep track of the player's statistics, and 
/// consists of methods that can affect the player's attributes.
/// </summary>
public class HealthManager : MonoBehaviour
{

    /// <summary>
    /// Singleton: Is a region used to create an instance of the HealthManager
    /// class
    /// </summary>
    #region Singleton
    public static HealthManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;

    /// <summary>
    /// Start(): Is a void method used for initialization
    /// </summary>
    void Start()
    {
 
    }

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
