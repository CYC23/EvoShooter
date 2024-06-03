
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

[System.Serializable]
public class SaveData {
    public int highScore;
    public int generation;
    public float[] parant;
    public float[] kid;
    public float fitness1;
    public float fitness2;


    public SaveData(int hs, int ge, float f1, float f2) {
        highScore = hs;
        generation = ge;    
        fitness1 = f1;
        fitness2 = f2;
    }
}
