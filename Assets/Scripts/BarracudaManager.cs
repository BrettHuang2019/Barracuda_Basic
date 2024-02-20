using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Barracuda;
using UnityEngine;
using UnityEngine.UI;

public class BarracudaManager : MonoBehaviour
{
    [SerializeField] private Texture2D numberImage;
    
    [Header("Model")] 
    [SerializeField] private NNModel model;

    [Header("References")]
    [SerializeField]private RawImage spriteRenderer;
    [SerializeField] private TextMeshProUGUI result;
    [SerializeField] private StringLogSO stringLogSO;
    

    private Model runtimeModel;
    private IWorker engine;
    private float[] predicted;
    
    private void Start()
    {
        runtimeModel = ModelLoader.Load(model);
        engine = WorkerFactory.CreateWorker(runtimeModel);
    }

    public void Process()
    {
        Tensor input = new Tensor(numberImage, 1);
        Tensor output = engine.Execute(input).PeekOutput();
        input.Dispose();

        predicted = output.AsFloats().SoftMax().ToArray();

        for (int i = 0; i < predicted.Length; i++)
        {
            Debug.Log($"{i}:  {predicted[i].ToString("0.##########")}");
        }

        spriteRenderer.texture = numberImage;
        result.text = Array.IndexOf(predicted, predicted.Max()).ToString();
    }

    private void PrintPredicted(float[] predicted)
    {
        string predictedStr = "";
        Array.Sort(predicted);
        Array.Reverse(predicted);
        
        for (int i = 0; i < predicted.Length; i++)
        {
            predictedStr += $"{i}:  {predicted[i].ToString("0.##########")}\n";
        }
    }

    private void OnDestroy()
    {
        engine?.Dispose();
    }

}