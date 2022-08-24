using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    // Refrence to the object that will control the rotation.
    [SerializeField] GameObject hand;

    //Serialized Variables debugging the difference between a raw angle and wrapped angle
    [SerializeField] float rawAngle;
    [SerializeField] float wrappedAngle;

    // Update is called once per frame
    void Update()
    {
        // The old  approaches are saved here so you can see how I came to the third approach, feel free to comment/uncomment and try them out

        // First approach - Rotates on all axes, Not what we want
        //gameObject.transform.rotation = hand.transform.rotation;

        // Second apporach - Create a new Quaternion and set Euler angles with a vector 3, then reassign the rotation.
        //Quaternion quaternion= new Quaternion();
        //quaternion.eulerAngles = new Vector3(0, 0, hand.transform.rotation.z);
        //gameObject.transform.rotation = quaternion;

        //Third approach -  Rotate a number of degrees on a given axis, all in one line without declaring a "new Quaternion()" every frame.
        gameObject.transform.rotation = Quaternion.AngleAxis(hand.transform.rotation.eulerAngles.z, Vector3.forward);

        //Wrapped angle example shows the difference between the getting a raw angle, and using some math to offset the angle
        WrapAngleExample();
    }

    void WrapAngleExample()
    {
        rawAngle = transform.rotation.eulerAngles.z;
        wrappedAngle = WrapAngle(transform.rotation.eulerAngles.z);

        //Debug.Log("Raw Angle value: " + rawAngle);        //Changed these to Serialized fields for better readability than debug statements in Update().
        //Debug.Log("Wrapped Angle value: " + wrappedAngle);
    }


    // Code from this Unity Forum post talking about how the Quaternion rotation and the values in the unity editor are different
    // https://forum.unity.com/threads/solved-how-to-get-rotation-value-that-is-in-the-inspector.460310/
    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;
        return angle;
    }

    private static float UnwrapAngle(float angle)
    {
        if (angle >= 0)
            return angle;

        angle = -angle % 360;

        return 360 - angle;
    }
}
