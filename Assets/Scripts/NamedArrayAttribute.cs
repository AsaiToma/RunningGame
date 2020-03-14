using UnityEngine;

public class NamingArrayAttribute : PropertyAttribute
{
    public readonly string[] names;
    public NamingArrayAttribute(string[] names) { this.names = names; }
}