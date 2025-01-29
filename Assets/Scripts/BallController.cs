using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject recallPoint;

    private LayerMask groundLayerMask;
    private int maxHeight = 10;
    public bool BallInPlay = false;

    private void Start(){
        groundLayerMask = LayerMask.GetMask("Ground");
    }

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
}
