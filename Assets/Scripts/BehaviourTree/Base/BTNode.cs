using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BehaviourTree
{
    /// <summary>
    /// Behaviour tree node state.
    /// </summary>
    public enum NodeState
    {
        SUCCESS,
        RUNNING,
        FAIL
    }

    /// <summary>
    /// The base class for any node.
    /// </summary>
    public abstract class BTNode : Node
    {
        [Input] public NodeState _in;               // Input state of the node.
        protected NodePort outPort;                 // Output port of the node.
        protected List<NodePort> connections;       // All node connections from output port.
        public bool IsCurrentNode { get; set; }     // Is currently active node?

#if UNITY_EDITOR
        [HideInInspector] public Color selectedNodeColor = new Color(.4f, .6f, .4f);    // Node color when active.
        [HideInInspector] public Color idleNodeColor = Color.black;                     // Node color when not-active.
        [HideInInspector] public bool useBaseColor = true;                              // Use base(XNode) node color?
#endif

        /// <summary>
        /// Initializes the node with a context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        public virtual void InitNode(BTContext context)
        {
            outPort = GetPort("_out");
            if (outPort != null)
                connections = outPort.GetConnections();
        }

        /// <summary>
        /// Returns the input node state.
        /// </summary>
        public NodeState GetNodeState() => _in;

        /// <summary>
        /// Executes the node with the context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        /// <returns>The state of the node</returns>
        public abstract NodeState Execute(BTContext context);

        /// <summary>
        /// Sets the current active node.
        /// </summary>
        public void SetCurrentActiveNode() => (graph as BTGraphBase).SetActiveNode(this);

        /// <summary>
        /// Returns the description of the node.
        /// </summary>
        public abstract string GetDescription();
    }
}
