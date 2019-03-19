using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {
    private RawImage m_RawImage;
    private GameManagerScript m_GMScript;
    private GameObject m_GM;
	// Use this for initialization
	void Start () {
        m_GM = GameObject.FindGameObjectWithTag("GameController");
        m_GMScript = m_GM.GetComponent<GameManagerScript>();
        m_RawImage = GetComponentInChildren<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_GMScript.HasScroll())
        {
            m_RawImage.color = new Vector4(255, 255, 255, 1);
        }
	}
}
