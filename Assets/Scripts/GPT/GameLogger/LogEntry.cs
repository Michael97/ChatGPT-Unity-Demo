using System;

public class LogEntry
{
    public LogType Type => m_type;
    private LogType m_type;

    public TimeSpan Timestamp => m_timestamp;
    private TimeSpan m_timestamp;

    public string Message => m_message;
    private string m_message;

    public LogEntry(LogType type, TimeSpan timestamp, string message)
    {
        m_type = type;
        m_timestamp = timestamp;
        m_message = message;
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
