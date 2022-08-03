using Entities;

/// <summary>
/// Context of any node.
/// </summary>
[System.Serializable]
public class BTContext
{
    public Entity owner { get; set; }                           // Entity that handles the behaviour tree.
    public BTContext(Entity owner) => this.owner = owner;       // Context constructor.
}
