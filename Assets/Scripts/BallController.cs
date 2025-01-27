using UnityEngine;
using MixedReality.Toolkit;
using MixedReality.Toolkit.Subsystems;
using UnityEngine.XR;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject recallPoint;

    private HandsAggregatorSubsystem aggregator;
    private LayerMask groundLayerMask;
    private int maxHeight = 10;
    public bool BallInPlay = false;

    public void OnBallReleased(){
        BallInPlay = true;
    }

    public void RecallBall(){
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.transform.position = recallPoint.transform.position;
        BallInPlay = false;
    }

    private void FixedUpdate(){
        if (BallInPlay){
            CheckForOutOfBounds();
        }
    }

    private void CheckForOutOfBounds(){
        RaycastHit hit;
        if (!Physics.Raycast(ball.transform.position, Vector3.down, out hit, maxHeight, groundLayerMask))
        { 
            RecallBall();
        }
    }

    private void Start(){
        aggregator = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    private void Update(){
        Test();
    }

    private void Test(){
        bool handIsValid = aggregator.TryGetPalmFacingAway(XRNode.LeftHand, out bool isLeftPalmFacingAway);
        if (isLeftPalmFacingAway){
            Debug.Log("Left palm facing away");
        }
    }
}
