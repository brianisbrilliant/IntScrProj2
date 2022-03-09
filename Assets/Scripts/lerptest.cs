using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class lerptest : MonoBehaviour
{

    public Transform destination;

    public AnimationCurve curve;

    public float returnInterval = 2;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        
    }

    void Update() {
        var keyboard = Keyboard.current;
        if(keyboard == null) return;

        if(keyboard.rKey.wasPressedThisFrame) {
            StartCoroutine(move());
        }
    }



    IEnumerator move() {
        float elapsedTime = 0f;
        while(elapsedTime < returnInterval) {
            transform.position = Vector3.Lerp(startPosition, destination.position, curve.Evaluate(elapsedTime / returnInterval));
            // transform.rotation = Quaternion.Lerp(endRotation, startingRotation, elapsedTime / returnInterval);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        elapsedTime = 0f;
        
        while(elapsedTime < returnInterval) {
            transform.position = Vector3.Lerp(destination.position, startPosition, curve.Evaluate(elapsedTime / returnInterval));
            // transform.rotation = Quaternion.Lerp(endRotation, startingRotation, elapsedTime / returnInterval);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
