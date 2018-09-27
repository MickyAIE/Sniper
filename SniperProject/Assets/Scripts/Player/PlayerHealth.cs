using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Animations;
public class PlayerHealth : MonoBehaviour {
    public float Health = 100f;
    private bool deaded = false;
    public Slider m_Slider;
    public AudioSource DeathNoise;
    public Animator animator;
    Scene m_Scene;
    string sceneName;
    // Use this for initialization
    void ReloadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    void Start () {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        
    }

    // Update is called once per frame
    void Update() {
        if (Health < 100f && Health > 0f)
        { Health += 0.02f; }
        m_Slider.value = Health;
        if (Health <= 0 && deaded == false)
        {
            animator.SetBool("Dead!", true);
            DeathNoise.Play();
            Invoke("ReloadScene", 3f);
        }
	}
    public void TakeDamage(float amount)
    {
        Health -= amount;
    }
}
