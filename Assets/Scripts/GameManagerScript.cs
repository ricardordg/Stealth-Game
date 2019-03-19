using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
    private bool m_HasScroll;

	void Awake () {
        m_HasScroll = false;
	}

    public void setHasScroll()
    {
        m_HasScroll = true;
    }

    public bool HasScroll()
    {
        return m_HasScroll;
    }
}
