using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {
    private GameObject m_GM;
    private GameManagerScript m_GMScript;

	void Awake () {
        m_GM = GameObject.FindGameObjectWithTag("GameController");
        m_GMScript = m_GM.GetComponent<GameManagerScript>();
	}
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && m_GMScript.HasScroll()){
            Debug.Log("Game Over");
            Application.LoadLevel(3);
        }
        else if (other.CompareTag("Player") && !m_GMScript.HasScroll())
        {
            Debug.Log("Voce não pegou o Scroll");
        }
    }
}
