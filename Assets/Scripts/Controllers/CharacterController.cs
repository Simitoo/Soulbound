using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 400f;

    [SerializeField]
    [Range(0, .3f)]
    private float movementSmoothing = .05f;

    [SerializeField] 
    private bool m_AirControl = false;

    [SerializeField]
    private LayerMask m_WhatIsGround;  
    
    [SerializeField]
    private Transform m_GroundCheck; 

    const float k_GroundedRadius = .2f; 
    private bool m_Grounded;            
    
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;  

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();        

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();    
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool jump)
    {       
        if (m_Grounded || m_AirControl)
        {                     
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, movementSmoothing);            

            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }

        if (m_Grounded && jump)
        {
            m_Grounded = true;
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));           
        }
    }

    private void Flip()
    {   
        m_FacingRight = !m_FacingRight; 

        transform.Rotate(0f, 180f, 0f);
    }   
}
