using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private HealthSystem _healthSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        _healthSystem = gameObject.GetComponent<HealthSystem>();
        _healthSystem.OnDeth += Die;
    }

    private void Die()
    {
        SceneManager.LoadScene(0);
    }
    
}
