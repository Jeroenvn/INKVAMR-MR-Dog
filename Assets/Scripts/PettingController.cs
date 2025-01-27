using MixedReality.Toolkit.SpatialManipulation;
using UnityEngine;
using UnityEngine.Events;

public class PettingController : MonoBehaviour
{
    public UnityEvent PettingStart = new();
    public UnityEvent PettingEnd = new();

    [SerializeField] private ObjectManipulator dogObjectManipulator;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;
    [SerializeField] private float pettingMovementThreshold = 0.008f;
    [SerializeField] private float secondsOfInactivityBeforeStoppingPetting = 0.5f;
    private Vector3 rightHandPosition;
    private Vector3 leftHandPosition;
    private float inactivityCountdown = 0f;

    private void FixedUpdate(){
        if (dogObjectManipulator.IsGrabHovered){
            if (Vector3.Distance(rightHand.position, rightHandPosition) > pettingMovementThreshold){
                if (inactivityCountdown <= 0){
                    PettingStart?.Invoke();
                }
                inactivityCountdown = secondsOfInactivityBeforeStoppingPetting;
            }
        }
        if (inactivityCountdown > 0){
            inactivityCountdown -= Time.deltaTime;
            if (inactivityCountdown <= 0){
                PettingEnd?.Invoke();
            }
        }
        rightHandPosition = rightHand.position;
    }
}
