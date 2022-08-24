using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kit;

public class TestBounds : MonoBehaviour
{
    // Reference to the object that goes into the bounds volume
    [SerializeField] public GameObject hand;

    // Variables for debugging & inspecting
    [SerializeField] Bounds _bounds;
    [SerializeField] Vector3 _boundsSize = Vector3.one;

    [SerializeField] private bool handInBounds = false;

    [SerializeField] private float _scaledValueX = 0;
    [SerializeField] private float _scaledValueY = 0;
    [SerializeField] private float _scaledValueZ = 0;
    [SerializeField] private float _distanceFromCenter = 0;

    // Update is called once per frame
    void Update()
    {
        UpdateBounds(); // Update bounds with any changes in position and scale

        // Note 1 -- The intial approach for checking our position within the bounds
        // Let's track how far from center we are as an inital approach 
        _distanceFromCenter = Vector3.Distance(transform.position, hand.transform.position);
        // Notice _distanceFromCenter prints from 0 (at the center) to 1/2 of the _bounds size (Ex. 0.5 if bounds size is 1)
        // This is close to what we want, but it would better if the values are mapped so that Lower=-1, Center=0, Max=1
        // Now we could multiply this the distance*2 so that  0.5 becomes 1 at the edges, but it doesnt account for the scale of the object.
        // We could multiply it times transform.localscale.x instead, but maybe there is a more direct approach. --> Checkout note 2 below

        CheckBounds();  // Check the bounds using the properties and functions of the bounds like bounds.center and Contains()

        if (handInBounds)
        {
            UpdateScaledValues();
        }
    }
    void UpdateBounds()
    {
        _bounds = new Bounds(transform.position, _boundsSize);
    }

    private void OnDrawGizmos()
    {
        GizmosExtend.DrawBounds(_bounds, Color.magenta);
    }

    void CheckBounds()
    {
        Vector3 handPosition = hand.transform.position;

        // We can easily check if the hand is inside the bounds using bounds.contains(pointInSpace)
        // And we can use a ternary operator to simplify the if else statements
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
        handInBounds = _bounds.Contains(handPosition) ? true : false;

        /* The line above does the exact same as the if statement below
        if (_bounds.Contains(handPosition))
        {
            //Debug.Log("The Cube is inside the bounds");
            handInBounds = true;
        }
        else
        {
            handInBounds = false;
        }*/

        if (handInBounds)
        {
            // Check the cube relative to the middle of the bounds
            if (handPosition.x > _bounds.center.x)
            {
                Debug.Log("Hand is in front of the Bounds Center");
            }
            if (handPosition.x < _bounds.center.x)
            {
                Debug.Log("Hand is behind the Bounds Center");
            }

            if (handPosition.y > _bounds.center.y)
            {
                Debug.Log("Hand is Above the Bounds Center");
            }
            if (handPosition.y < _bounds.center.y)
            {
                Debug.Log("Hand is Below the Bounds Center");
            }

            if (handPosition.z > _bounds.center.z)
            {
                Debug.Log("Hand is left of the Bounds Center");
            }
            if (handPosition.z < _bounds.center.z)
            {
                Debug.Log("Hand is right of the Bounds Center");
            }
        }
    }

    private void UpdateScaledValues()
    {
        // Note 2 -- Mapping a value on a scale between 0 and 1 with min and max range.
        // Here we can use the mathmatical function for normalizing a value. See the equation above GetScaledValue and more details in the stack overflow post.
        _scaledValueX = GetScaledValue(hand.transform.position.x,   //rawValue = Hand's current position in worldspace
                                        transform.position.x,       //minValue = Center of bounds, this game object transform
                                        _bounds.extents.x,          //maxValue = Edge of bounds, use extents of the bounds
                                        transform.position.x);      //centerWorldSpacePos = center of the bounds, this game object transform

        _scaledValueZ = GetScaledValue(hand.transform.position.z,   //rawValue = Hand's current position
                                        transform.position.z,       //minValue = Center of bounds, this transform
                                        _bounds.extents.z,          //maxValue = Edge of bounds, use extents
                                        transform.position.z);      //centerWorldSpacePos = center of the bounds, this transform

        _scaledValueY = GetScaledValue(hand.transform.position.y,   //rawValue = Hand's current position
                                        transform.position.y,       //minValue = Center of bounds, this transform
                                        _bounds.extents.y,          //maxValue = Edge of bounds, use extents
                                        transform.position.y);      //centerWorldSpacePos = center of the bounds, this transform

        // Note 3 -- We can specify any min and max value
        // We can change the min and max range if we wish, we dont need to start at the middle of the bounds and end at the extents of the bounds. 
        // For example, above we assume the center of the bounds at as the minimum. What if we want to map the y axis from the bottom of the bounds to the top of the bounds?
        // We can pass a different miniumum value instead. Here we can use the inverse of the bounds extents to get the lower bounds

        /*
        _scaledValueY = GetScaledValue(hand.transform.position.y,   //rawValue = Hand's current position
                                        -_bounds.extents.y,         //minValue = lower edge of bounds, use negative extents
                                        _bounds.extents.y,          //maxValue = max edge of bounds, use extents
                                        transform.position.y);      //centerWorldSpacePos = center of the bounds, this transform
        */
    }


    // The equation for a scaled value is z = value(x) - min(x) / max(x) - min(x)
    // https://stats.stackexchange.com/questions/70801/how-to-normalize-data-to-0-1-range
    float GetScaledValue(float rawValue, float minValue, float maxValue, float centerWorldSpacePos = 0.0f)
    {
        float returnScaledValue = 0f;

        // Note 4 -- Working relative to world space
        // The code will work right now because the bounds center is at the world center, but if you move the bounds area notice the scaled values dont work as expected anymore.

        // To correct for this, need to add the worldspace centerPosition to change the min and max values from being a relative number (3.5) to 
        //(Ex: maxValue=3.5) plus the worldspace coordinate (Ex: center=Vector3(20, 1, -30)) so you get a worldspaceMax=Vector3(20, 4.5, -30)

        //minValue += centerWorldSpacePos;
        //maxValue += centerWorldSpacePos;

        if (minValue == 0f)
        {
            returnScaledValue = rawValue / maxValue;
        }
        else
        {
            returnScaledValue = (rawValue - minValue) / (maxValue - minValue);
        }
        return returnScaledValue;

    }
}
