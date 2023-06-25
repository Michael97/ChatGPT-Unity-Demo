using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ResponseBuilder : MonoBehaviour
{
    private List<string> executedFunctions;
    private Dictionary<string, string> functionResults;

    public ResponseBuilder()
    {
        executedFunctions = new List<string>();
        functionResults = new Dictionary<string, string>();
    }

    public void AddExecutedFunction(string functionName)
    {
        executedFunctions.Add(functionName);
    }

    public void AddFunctionResult(string functionName, string result)
    {
        functionResults[functionName] = result;
    }

    public string BuildResponse()
    {
        StringBuilder response = new StringBuilder();

        foreach (string functionName in executedFunctions)
        {
            response.AppendLine($"{functionName} executed.");
            if (functionResults.TryGetValue(functionName, out string result))
            {
                response.AppendLine($"Result: {result}");
            }
            else
            {
                response.AppendLine("No result available.");
            }
        }

        ClearData();

        return response.ToString();
    }

    private void ClearData()
    {
        executedFunctions.Clear();
        functionResults.Clear();
    }
}
