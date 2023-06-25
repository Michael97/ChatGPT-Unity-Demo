using System;

public class LogEntry
{
    public LogType Type { get; }
    public TimeSpan Timestamp { get; }
    public string Message { get; }

    public LogEntry(LogType type, TimeSpan timestamp, string message)
    {
        Type = type;
        Timestamp = timestamp;
        Message = message;
    }

    public override string ToString()
    {
        return $"[{Timestamp}] {Type}: {Message}";
    }
}

public enum LogType
{
    ToChatGpt,
    FromChatGpt,
    FunctionRequest,
    FunctionExecution,
    Low,
    Medium,
    High
}
