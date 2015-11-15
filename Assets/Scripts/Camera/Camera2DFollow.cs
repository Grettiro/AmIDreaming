using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
	public Transform target;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;

	private float m_offsetZ;
	private Vector3 m_lastTargetPosition;
	private Vector3 m_currentVelocity;
	private Vector3 m_lookAheadPos;

	// Use this for initialization
	private void Start()
	{
	    m_lastTargetPosition = target.position;
	    m_offsetZ = (transform.position - target.position).z;
	    transform.parent = null;
		transform.position = target.position;
	}

	// Update is called once per frame
	private void Update()
	{

	    // only update lookahead pos if accelerating or changed direction
	    float xMoveDelta = (target.position - m_lastTargetPosition).x;

	    bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

	    if (updateLookAheadTarget)
	        m_lookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
	    else
	        m_lookAheadPos = Vector3.MoveTowards(m_lookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);

	    Vector3 aheadTargetPos = target.position + m_lookAheadPos + Vector3.forward*m_offsetZ;
	    Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_currentVelocity, damping);

	    transform.position = newPos;

	    m_lastTargetPosition = target.position;
	}
}
