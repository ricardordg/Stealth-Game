using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour {
    public float m_Speed = 3f;
    public float m_TurnSpeed = 100f;

    //private float m_DampTime = 0.1f;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private Rigidbody m_Rigidbody;
    //private Animator m_Animator;

	void Awake () 
    {
        m_Rigidbody = GetComponent<Rigidbody>();
       // m_Animator = GetComponent<Animator>();
	}
	
	void Update () 
    {
        if (Input.GetButton("Run"))
        {
            m_Speed = 6f;
        }
        else m_Speed = 3f;

        m_MovementInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");
	}

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move()
    {
        Vector3 movement = new Vector3(transform.forward.x,0f,transform.forward.z) * m_MovementInputValue * m_Speed * Time.deltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}
