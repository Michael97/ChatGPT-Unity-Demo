using System.Collections.Generic;
using System.Linq;

public class Memory
{
    private Dictionary<string, string> memory;

    public Memory()
    {
        memory = new Dictionary<string, string>();
    }

    public string AddMemory(string key, string value)
    {
        if (memory.ContainsKey(key))
        {
            return $"Attempt to add memory key {key} failed, key already exists. Please try again with a new key.";
        }

        memory[key] = value;
        return $"Memory {key} added successfully.";
    }

    public string GetMemory(string key)
    {
        if (memory.ContainsKey(key))
        {
            return memory[key];
        }

        return $"Memory {key} not found.";
    }

    public Dictionary<string, string> GetMemories(int maxCount)
    {
        return memory.Take(maxCount).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public List<string> GetAllMemoryNames()
    {
        return memory.Keys.ToList();
    }
}
