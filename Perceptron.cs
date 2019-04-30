using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainingSet
{
    public double[] input;
    public double output;
}

public class Perceptron : MonoBehaviour
{
    public TrainingSet[] TS;
    double[] Weights = { 0, 0 };
    double bias = 0;
    float Total_Errors = 0;

    //DotProduct is a part of activation Function;
    double DotProductBias(double[] weights,double[] inputs) //(input_0 * weight_0 + input_1 * weight_1 + bias);
    {
        if (weights == null || inputs == null)
            return -1;

        if (weights.Length != inputs.Length)
            return -1;

        double d = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            d += weights[i] * inputs[i];
        }
        d += bias;

        return d;
    }

    double CalculateOutput(int i)//Activation Function f(x) = { 1 if weight * input + bias > 0;
                                //                              0 other wise;}
    {
        double dp = DotProductBias(Weights, TS[i].input);
        if (dp > 0) return 1;
        return 0;
    }

    double CalculateOutput(double input1,double input2)
    {
        double[] ip = new double[] { input1, input2 };
        double d = DotProductBias(Weights, ip);
        if (d > 0) return 1;
        return 0;
    }

    void InitializesWeights()
    {
        for (int i = 0; i < Weights.Length; i++)
        {
            Weights[i] = Random.Range(-1.0f,1.0f);
        }
        bias = Random.Range(-1.0f, 1.0f);
    }

    void UpdateWeights(int j)
    {
        double error = TS[j].output - CalculateOutput(j);
        Total_Errors += Mathf.Abs((float)error);
        for (int i = 0; i < Weights.Length; i++)
        {
            Weights[i] = Weights[i] + error * TS[j].input[i];
        }
        bias += error;
    }

    void Train(int Epochs)
    {
        InitializesWeights();
        for (int e = 0; e < Epochs; e++)
        {
            Total_Errors = 0;
            for (int i = 0; i < TS.Length; i++)
            {
                UpdateWeights(i);
                Debug.Log("W1: " + (Weights[0]).ToString("F2") + " W2: " + (Weights[1]).ToString("F2") + " Bias: " + bias.ToString("F2"));
            }
            Debug.Log("Total Errors: " + Total_Errors);
        }
    }
    
    void Start()
    {
        Train(8);
        Debug.Log("Test 0 0 : " + CalculateOutput(0, 0));
        Debug.Log("Test 0 1 : " + CalculateOutput(0, 1));
        Debug.Log("Test 1 0 : " + CalculateOutput(1, 0));
        Debug.Log("Test 1 1 : " + CalculateOutput(1, 1));
    }

}
