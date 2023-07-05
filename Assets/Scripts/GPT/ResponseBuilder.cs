using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ResponseBuilder : MonoBehaviour
{
    private List<string> m_executedFunctions;
    private Dictionary<string, string> m_functionResults;

    public ResponseBuilder()
    {
        m_executedFunctions = new List<string>();
        m_functionResults = new Dictionary<string, string>();
    }

    public void AddExecutedFunction(string functionName)
    {
        m_executedFunctions.Add(functionName);
    }

    public void AddFunctionResult(string functionName, string result)
    {
        m_functionResults[functionName] = result;
    }

    public string BuildResponse()
    {
        StringBuilder response = new StringBuilder();

        foreach (string functionName in m_executedFunctions)
        {
            response.AppendLine($"{functionName} executed.");
            if (m_functionResults.TryGetValue(functionName, out string result))
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
        m_executedFunctions.Clear();
        m_functionResults.Clear();
    }
}
