using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public int m_StartingHealth = 100;
    public int m_CurrentHealth;
    public Slider m_Slider;

    private GameObject m_GM;
    private AudioSource m_Audio;

    private bool m_IsDead;

	void Awake () {
        m_GM = GameObject.FindGameObjectWithTag("GameController");
        m_Audio = m_GM.GetComponent<AudioSource>();
        m_CurrentHealth = m_StartingHealth;
	}
	
	void Update () {
        
	}

    public void TakeDamage(int amount)
    {
        m_CurrentHealth -= amount;

        m_Slider.value = m_CurrentHealth;

        if (m_CurrentHealth <= 0 && !m_IsDead)
        {
            Death();
        }
    }

    void Death()
    {
        m_IsDead = true;

        Time.timeScale = 0;
        m_Audio.Stop();
        Application.LoadLevel(2);

        //anim.setTrigger("Die");
    }
}
