using System.Collections.Generic;
using System.Linq;

public class Memory
{
    private Dictionary<string, string> m_memory;

    public Memory()
    {
        m_memory = new Dictionary<string, string>();
    }

    public string AddMemory(string key, string value)
    {
        if (m_memory.ContainsKey(key))
        {
            return $"Attempt to add memory key {key} failed, key already exists. Please try again with a new key.";
        }

        m_memory[key] = value;
        return $"Memory {key} added successfully.";
    }

    public string GetMemory(string key)
    {
        if (m_memory.ContainsKey(key))
        {
            return m_memory[key];
        }

        return $"Memory {key} not found.";
    }

    public Dictionary<string, string> GetMemories(int maxCount)
    {
        return m_memory.Take(maxCount).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public List<string> GetAllMemoryNames()
    {
        return m_memory.Keys.ToList();
    }
}
