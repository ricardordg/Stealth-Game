using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {
    public float m_TimeBetweenBullets = 2f;
    public float m_Range = 100;
    public float m_EffectsDisplayTime = 0.1f;
    public float m_Timer;

    private int m_DamagePerShot = 40;
    private Ray m_ShootRay;
    private RaycastHit m_ShootHit;
    private LineRenderer m_Line;
    private Light m_Light;
    private AudioSource m_Audio;
    private int m_ShootableMask;

    void Awake()
    {
        m_ShootableMask = LayerMask.GetMask("Shootable");
        m_Audio = GetComponent<AudioSource>();
        m_Line = GetComponent<LineRenderer>();
        m_Light = GetComponent<Light>();
    }

	void Start () {
	
	}
	
	void Update () {
        m_Timer += Time.deltaTime;

        if (m_Timer >= m_TimeBetweenBullets * m_EffectsDisplayTime)
        {
            DisableEffects();
        }
	}

    public void Shoot()
    {
        m_Timer = 0f;

        m_Audio.Play();

        m_Light.enabled = true;

        m_Line.enabled = true;
        m_Line.SetPosition(0, transform.position);

        m_ShootRay.origin = transform.position;
        m_ShootRay.direction = transform.forward;

        if (Physics.Raycast(m_ShootRay, out m_ShootHit, m_Range, m_ShootableMask))
        {
            PlayerHealth playerHealth = m_ShootHit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(m_DamagePerShot);
            }
            m_Line.SetPosition(1, m_ShootHit.point);
        } else
            m_Line.SetPosition(1, m_ShootRay.origin + m_ShootRay.direction * m_Range);
    }

    void DisableEffects()
    {
        m_Line.enabled = false;
        m_Light.enabled = false;   
    }
}
