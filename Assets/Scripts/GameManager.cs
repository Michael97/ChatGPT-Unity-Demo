using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ChatGptAgent chatGptAgent;
    [SerializeField] private ResponseParser responseParser;
    [SerializeField] private string initialPromptPath;


}
