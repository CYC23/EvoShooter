using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Gene : MonoBehaviour
{
    //public static SettingData options = new();
    //public readonly static string SETTINGS_FILENAME = "GamesSettings.json";
    //public readonly static string SETTINGS_PATH = Path.Combine(Application.dataPath, SETTINGS_FILENAME);

    public int HealthPoint;
    public int AttackPoint;
    public float FireRate;
    public int MagazineSize;
    public float MoveSpeed;
    public float RotateSpeed;
    public float ViewDistance;
    public static List<float> array = new List<float>();

    public static double mutationStrength;
    static System.Random rand = new System.Random();
    public static int limitation;
    public static int generation;


    public void Loading()
    {
        array = new();
        //GameSettings.LoadSettings();
        HealthPoint = GameSettings.options.Agent_HealthPoint;
        AttackPoint = GameSettings.options.Agent_AttackPoint;
        FireRate = GameSettings.options.Agent_FireRate;
        MagazineSize = GameSettings.options.Agent_MagazineSize; 
        MoveSpeed = GameSettings.options.Agent_MoveSpeed;
        RotateSpeed = GameSettings.options.Agent_RotateSpeed;
        ViewDistance = GameSettings.options.Agent_ViewDistance;

        // Add data to array list
        array.Add(HealthPoint);
        array.Add(AttackPoint);
        array.Add(FireRate);
        array.Add(MagazineSize);
        array.Add(MoveSpeed);
        array.Add(RotateSpeed);
        array.Add(ViewDistance);
        array.Add(limitation - array.Sum());

        //Debug.Log(array.ToArray()[0]);
        Debug.Log("Array Contents: " + string.Join(", ", array.ToArray()));
    }

    private void Awake()
    {
        limitation = 80;
        mutationStrength = 5;
        generation = 100;
        Loading();
        
        Debug.Log(string.Format("limitation: {0}, mutationStrength: {1}", limitation, mutationStrength));
    }

    private float Fitness(float[] individual)
    {
        //float f1 = -individual.Take(individual.Length - 1).Sum(x => x * x); // Example objective function to minimize
        float f1 = 0;
        for(int i = 0; i < individual.Length-1; i++)
        {
            f1 += individual[i]*(i+1) * 0.1f;
        }
        return f1;
    }

    public void Test()
    {
        //Loading();
        
        float[] parent = array.ToArray();
        Debug.Log("parent: " + string.Join(", ", parent)+" Fitness: "+ Fitness(parent));
        float[] kids = Mutate(parent, limitation);
        Debug.Log("kids: " + string.Join(", ", kids) + " Fitness: " + Fitness(kids));
        float[] population = Environment(parent, kids);
        Debug.Log("result: " + string.Join(", ", population));
        Debug.Log(string.Format("mutationStrength: {0}",mutationStrength));
    }

    public float[] Mutate(float[] parent, int limitation)
    {
        int n = parent.Length;
        float[] newIndividual;

        do
        {
            float[] mutation = new float[n];
            for (int i = 0; i < n; i++)
            {
                mutation[i] = (float)(mutationStrength * NextGaussian());
            }

            newIndividual = parent.Zip(mutation, (x, m) => Math.Max(0, x + m)).ToArray();
            // Round each element in newIndividual to the nearest integer
            newIndividual = newIndividual.Select(x => (float)Math.Round(x)).ToArray();

        } while (newIndividual.Sum() != limitation);

        return newIndividual;
    }

    public float[] Environment(float[] parents, float[] kids)
    {
        float fp = Fitness(parents);
        float fk = Fitness(kids);
        Debug.Log(string.Format("fp: {0}, fk: {1}", fp, fk));
        float pTarget = 1.0f / 5.0f;
        int n = parents.Length;

        if (fp < fk) // kid better than parent
        {
            mutationStrength *= Math.Exp(1.0 / Math.Sqrt(n + 1) * (1 - pTarget) / (1 - pTarget)); // adjust global mutation strength
            return kids;
        }
        else
        {
            mutationStrength *= Math.Exp(1.0 / Math.Sqrt(n + 1) * (0 - pTarget) / (1 - pTarget)); // adjust global mutation strength
            return parents;
        }
    }

    static double NextGaussian()
    {
        // Using Box-Muller transform to generate a standard normal distribution (mean=0, variance=1)
        double u1 = 1.0 - rand.NextDouble(); // uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); // random normal(0,1)
        return randStdNormal;
    }
}

