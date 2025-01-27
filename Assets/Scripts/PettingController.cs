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
        CheckForPets();
        CountDown();
        UpdateHandPositions();
    }

    private void CheckForPets(){
        bool isTouching = dogObjectManipulator.IsGrabHovered;
        if (!isTouching){
            return;
        }

        // Yes, this does not check which hand is petting the dog
        bool isMoving = Vector3.Distance(rightHand.position, rightHandPosition) > pettingMovementThreshold || Vector3.Distance(leftHand.position, leftHandPosition) > pettingMovementThreshold;
        if (!isMoving){
            return;
        }

        if (inactivityCountdown <= 0){
            PettingStart?.Invoke();
        }
        inactivityCountdown = secondsOfInactivityBeforeStoppingPetting;
    }

    private void CountDown(){
        if (inactivityCountdown <= 0){
            return;
        }
        
        inactivityCountdown -= Time.deltaTime;
        if (inactivityCountdown <= 0){
            PettingEnd?.Invoke();
        }
    }

    private void UpdateHandPositions(){
        rightHandPosition = rightHand.position;
        leftHandPosition = leftHand.position;
    }
}
