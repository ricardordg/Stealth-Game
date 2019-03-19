using UnityEngine;
using System.Collections;

public class ScrollScript : MonoBehaviour {
    private GameObject m_Canvas;
    private GameManagerScript m_GMScript;
    private GameObject m_GM;
	// Use this for initialization
	void Awake () {
        m_GM = GameObject.FindGameObjectWithTag("GameController");
        m_Canvas = GameObject.FindGameObjectWithTag("ScrollCanvas");
        m_GMScript = m_GM.GetComponent<GameManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_GMScript.setHasScroll();
            Destroy(m_Canvas);
            Destroy(gameObject);
        }
    }
}
