//using UnityEngine;
//using UnityEngine.Events;

//public class PlayerController : MonoBehaviour
//{
//    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
//    [SerializeField] private LayerMask m_WhatIsWall;
//    [SerializeField] private Transform m_CollisionCheck;

//    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
//    private Rigidbody2D m_Rigidbody2D;
//    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
//    private Vector3 m_Velocity = Vector3.zero;

//    [Header("Events")]
//    [Space]

//    public UnityEvent OnLandEvent;

//    [System.Serializable]
//    public class BoolEvent : UnityEvent<bool> { }

//    public BoolEvent OnCrouchEvent;
//    private bool m_wasCrouching = false;

//    private void Awake()
//    {
//        m_Rigidbody2D = GetComponent<Rigidbody2D>();

//        if (OnLandEvent == null)
//            OnLandEvent = new UnityEvent();

//        if (OnCrouchEvent == null)
//            OnCrouchEvent = new BoolEvent();
//    }

//    private void FixedUpdate()
//    {

//        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_CollisionCheck.position, m_WhatIsWall);
//        for (int i = 0; i < colliders.Length; i++)
//        {
//            if (colliders[i].gameObject != gameObject)
//            {
//            }
//        }
//    }


//    public void Move(float move, bool crouch, bool jump)
//    {
//        // Move the character by finding the target velocity
//        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
//        // And then smoothing it out and applying it to the character
//        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

//        // If the input is moving the player right and the player is facing left...
//        if (move > 0 && !m_FacingRight)
//        {
//            // ... flip the player.
//            Flip();
//        }
//        // Otherwise if the input is moving the player left and the player is facing right...
//        else if (move < 0 && m_FacingRight)
//        {
//            // ... flip the player.
//            Flip();
//        }
//    }


//    private void Flip()
//    {
//        // Switch the way the player is labelled as facing.
//        m_FacingRight = !m_FacingRight;

//        // Multiply the player's x local scale by -1.
//        Vector3 theScale = transform.localScale;
//        theScale.x *= -1;
//        transform.localScale = theScale;
//    }
//}