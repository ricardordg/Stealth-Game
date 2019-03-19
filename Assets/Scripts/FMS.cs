using UnityEngine;
using System.Collections;

public class FMS : MonoBehaviour {
    public Transform[] m_Targets;
    public float m_AngleView = 110f;
    
    private int m_NextTarget = 0;
    private int m_State;
    private UnityEngine.AI.NavMeshAgent m_NavMeshAgent;
    private SphereCollider m_SphereCollider;
    private GameObject m_Player;
    private bool m_InSight;
    private Transform m_LastPosition;
    private Animator m_Anim;
    private EnemyShooting m_Shooting;

    void Awake()
    {
        m_NavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_SphereCollider = GetComponent<SphereCollider>();
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_State = 1;
        m_Anim = GetComponent<Animator>();
        m_Shooting = GetComponentInChildren<EnemyShooting>();
    }

	void Start ()
    {
        GoToNextTarget();
	}
	
	void Update ()
    {
        switch (m_State)
        {
            case 1: Patrol(); 
                m_Anim.SetInteger("State", 1);
                if (m_InSight)
                    m_State = 2;
                break;
            case 2: Shoot(); 
                m_Anim.SetInteger("State", 2);
                if (!m_InSight)
                {
                    m_State = 3;
                    m_NavMeshAgent.SetDestination(m_LastPosition.position);
                }
                break;
            case 3: Chase(); 
                m_Anim.SetInteger("State", 3);
                if (m_InSight)
                {
                    m_State = 2;
                }
                else if (m_NavMeshAgent.remainingDistance < 0.1f)
                    m_State = 1;
                    
                break;
        }	
	}

    void Patrol()
    {
        if(m_NavMeshAgent.remainingDistance < 0.1f || m_NavMeshAgent.speed < 0.05f)
           GoToNextTarget();
    }

    void Shoot()
    {
        Vector3 direction = m_LastPosition.position - transform.position;
        direction.Normalize();

        m_NavMeshAgent.Stop();
        transform.forward = new Vector3(direction.x, transform.forward.y, direction.z);

        if (m_Shooting.m_Timer >= m_Shooting.m_TimeBetweenBullets && Time.timeScale != 0)
        {
            m_Shooting.Shoot();
        }

    }

    void Chase()
    {
        m_NavMeshAgent.Resume();    
    }

    void GoToNextTarget()
    {
        if (m_Targets.Length == 0) return;

        m_NavMeshAgent.SetDestination(m_Targets[m_NextTarget].position);

        m_NextTarget = (m_NextTarget + 1) % m_Targets.Length;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == m_Player)
        {
            m_InSight = false;

            Vector3 direction = new Vector3(other.transform.position.x - transform.position.x, 0f,other.transform.position.z - transform.position.z) ;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < m_AngleView * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, direction.normalized, out hit, m_SphereCollider.radius))
                {
                    if (hit.collider.gameObject == m_Player)
                    {
                        m_InSight = true;
                        m_LastPosition = other.gameObject.transform;
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == m_Player)
        {
            m_InSight = false;
        }
    }
}
