using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public GameObject holdsWheel;
    RadialLayoutGroup wheel;
    static List<Transform> children;
    static Dictionary<Transform, string> steps = new Dictionary<Transform, string>();
    public TextMeshProUGUI path;

    int countChild;

    bool shouldRotate = false;
    bool rotateForward = false;
    float degreesRotatedAlready = 0;
    int distanceBetweenPoints;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        wheel = holdsWheel.GetComponent(typeof(RadialLayoutGroup)) as RadialLayoutGroup;
        children = holdsWheel.GetComponentsInChildren<Transform>().ToList();
        distanceBetweenPoints = 360 / (children.Count - 1);
        Debug.Log(distanceBetweenPoints);

        //setting all child for the wheel
        int stepnum = 0;
        foreach(var child in children)
        {
            steps.Add(child, "Step " + stepnum + ":");
            stepnum++;
            Debug.Log(child.name);
        }

        //set the wheel
        countChild = 1;
        wheel.Offset = 360;
        path.text = steps[children[countChild]];
        children[countChild].localScale = new Vector3(2, 2, 2);
    }

    // Update is called once per frame
    void Update()
    { 
        if (shouldRotate && !rotateForward) //back
        {

            wheel.Offset += 0.5f * rotationSpeed;
            degreesRotatedAlready += 0.5f * rotationSpeed;
            //Debug.Log(wheel.Offset);

            if (degreesRotatedAlready >= distanceBetweenPoints)
            { //Stop rotating
                shouldRotate = false;
                rotateForward = false;
                degreesRotatedAlready = 0;

                path.text = steps[children[countChild]];
                children[countChild].localScale = new Vector3(2, 2, 2);
                children[countChild + 1].localScale = new Vector3(1, 1, 1);
                Debug.Log("Stopped rotation");
            }

        }
        else if (shouldRotate && rotateForward) //forward
        {

            wheel.Offset -= 0.5f * rotationSpeed;
            degreesRotatedAlready += 0.5f * rotationSpeed;
            //Debug.Log(wheel.Offset);

            if (degreesRotatedAlready >= distanceBetweenPoints)
            { //Stop rotating
                shouldRotate = false;
                rotateForward = false;
                degreesRotatedAlready = 0;

                path.text = steps[children[countChild]];
                children[countChild].localScale = new Vector3(2, 2, 2);
                children[countChild - 1].localScale = new Vector3(1, 1, 1);
                Debug.Log("Stopped rotation");
            }
        }
    }

    public void onClickForward()
    {
        if(countChild < children.Count - 1)
        {
            countChild++;

            shouldRotate = true;
            rotateForward = true;
            degreesRotatedAlready = 0;
        }
    }
    public void onClickBack()
    {
        if (countChild > 1)
        {
            countChild--;

            shouldRotate = true;
            rotateForward = false;
            degreesRotatedAlready = 0;
        }
    }
}



    