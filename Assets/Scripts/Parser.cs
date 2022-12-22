using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;


public class Parser
{
    string json; //= File.ReadAllText(Application.dataPath + "/Scripts/cammrad_sample_json.json");
    public Instructions stepsParse;

    public Parser(string filePath)
    {
        json = File.ReadAllText(filePath);
        stepsParse = JsonConvert.DeserializeObject<Instructions>(json);
    }
}

public struct Instructions
{
    public Dictionary<string, List<Step>> Instruct { get; set; }
}

public struct Step
{
    public string stepName { get; set; }
    public string iconFilePath { get; set; }
    public string stepInstruction { get; set; }
}

