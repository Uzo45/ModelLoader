using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    public GameObject holdsWheel;
    static List<Transform> children;
    static Dictionary<Transform, string> steps = new Dictionary<Transform, string>();
    public TextMeshProUGUI StepOutput;

    int countChild;

    static Parser parser = new Parser(Application.dataPath + "/RadialLayoutGroup/json2.json");
    List<Step> stuff = parser.stepsParse.Instruct["Somethingsteps"];

    bool shouldRotate = false;
    bool rotateForward = false;
    float degreesRotatedAlready = 0;
    int distanceBetweenPoints;
    public float rotationSpeed;

    void Awake()
    {
        ParseAndSetUpWheel();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupDictionary();
        //set the wheel
        countChild = 1;
        StepOutput.text = steps[children[countChild]];
        children[countChild].localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !shouldRotate)
        {
            onClickBack();
            Debug.Log("back");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !shouldRotate)
        {
            onClickForward();
            Debug.Log("forward");
        }

        if (shouldRotate && !rotateForward) //back
        {

            //wheel.Offset += 0.5f * rotationSpeed;

            holdsWheel.transform.Rotate(Vector3.forward, 0.5f * rotationSpeed);
            foreach(var icon in steps)
            {
                icon.Key.Rotate(Vector3.forward, -0.5f * rotationSpeed);
            }

            degreesRotatedAlready += 0.5f * rotationSpeed;
            //Debug.Log(wheel.Offset);

            if (degreesRotatedAlready >= distanceBetweenPoints)
            { //Stop rotating
                shouldRotate = false;
                rotateForward = false;
                degreesRotatedAlready = 0;

                StepOutput.text = steps[children[countChild]];
                children[countChild].localScale = new Vector3(1, 1, 1);
                children[countChild + 1].localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Debug.Log("Stopped rotation");
            }

        }
        else if (shouldRotate && rotateForward) //forward
        {

            //wheel.Offset -= 0.5f * rotationSpeed;
            
            holdsWheel.transform.Rotate(Vector3.forward, -0.5f * rotationSpeed);
            foreach (var icon in steps)
            {
                icon.Key.Rotate(Vector3.forward, 0.5f * rotationSpeed);
            }

            degreesRotatedAlready += 0.5f * rotationSpeed;
            //Debug.Log(wheel.Offset);

            if (degreesRotatedAlready >= distanceBetweenPoints)
            { //Stop rotating
                shouldRotate = false;
                rotateForward = false;
                degreesRotatedAlready = 0;

                StepOutput.text = steps[children[countChild]];
                children[countChild].localScale = new Vector3(1, 1, 1);
                children[countChild - 1].localScale = new Vector3(0.5f, 0.5f, 0.5f);
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

    void ParseAndSetUpWheel()
    {
        GameObject newStep;

        foreach (var i in stuff)
        {
            //give it an icon
            if (File.Exists(i.iconFilePath))
            {
                newStep = new GameObject();
                newStep.name = i.stepName;
                newStep.transform.parent = holdsWheel.transform;

                newStep.AddComponent<CanvasRenderer>();
                newStep.AddComponent<Image>();
                Image image = newStep.GetComponent<Image>();
                Davinci.get().load(i.iconFilePath).into(image).start();
            }
            else
            {
                Debug.Log("The path does not exist");
            }
        }
    }

    void SetupDictionary()
    {
        children = holdsWheel.GetComponentsInChildren<Transform>().ToList();

        //set each child as a key for its step name and instruction
        for (int i = 1; i < children.Count; i++)
        {
            children[i].localScale = new Vector3(0.5f, 0.5f, 0.5f);
            steps.Add(children[i], stuff[i - 1].stepName + ": " + stuff[i - 1].stepInstruction);
            Debug.Log(children[i].ToString());
        }

        distanceBetweenPoints = 360 / (children.Count - 1);
        Debug.Log(distanceBetweenPoints);
    }
}



    