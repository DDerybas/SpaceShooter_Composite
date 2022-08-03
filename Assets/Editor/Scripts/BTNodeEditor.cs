using UnityEngine;
using UnityEditor;
using XNodeEditor;
using BehaviourTree;

/// <summary>
/// A class that highlights the current active node. Also draws a description on the node.
/// </summary>
[CustomNodeEditor(typeof(BTNode))]
public class BehaviourNodeEditor : NodeEditor
{
    private Color descriptionColor = new Color(.9f, .9f, .9f);          // Description text color.
    private Color backgroundColor = new Color(0, 0, 0, .1f);            // Text label background color.
    private int textMarginVer = 2;                                      // Text vertical margin.
    private int textMarginHor = 0;                                      // Text horizontal margin.
    private int labelHeight = 30;                                       // Text label height.

    private GUIStyle bodyGuiStyle = new GUIStyle();                     // Text label style.
    private Texture2D texture = new Texture2D(1, 1);                    // Text label background texture.
    private BTNode node;                                                // Target node.

    /// <summary>
    /// Returns node tint color.
    /// </summary>
    public override Color GetTint()
    {
        node = target as BTNode;

        if(node == ((BTGraphBase)node.graph).currentActiveNode)
            return node.selectedNodeColor;
        else return node.useBaseColor ? base.GetTint() : node.idleNodeColor;
    }

    /// <summary>
    /// Draws node and node label.
    /// </summary>
    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        node = target as BTNode;
        EditorGUILayout.LabelField(node.GetDescription(), bodyGuiStyle);
        NodeEditorWindow.RepaintAll();
    }

    /// <summary>
    /// Sets the title of the node.
    /// </summary>
    public override void OnHeaderGUI()
    {
        bool isPlayingAndNode = (Application.isPlaying && node != null && node.IsCurrentNode);
        string label = $"{target.name}" +
            $" {(isPlayingAndNode ? "(" + node.GetNodeState() + ")" : string.Empty)}";
        GUILayout.Label(label, NodeEditorResources.styles.nodeHeader, GUILayout.Height(labelHeight));
    }

    /// <summary>
    /// Specifies the style of the description text and the background of the description text.
    /// </summary>
    public override void OnCreate()
    {
        base.OnCreate();
        node = target as BTNode;
        SetColoredBackground(backgroundColor, node.IsCurrentNode);
    }

    /// <summary>
    /// Specifies the style of the description text and the background of the description text.
    /// </summary>
    private void SetColoredBackground(Color color, bool isCurrent)
    {
        if (texture == null)
            texture = new Texture2D(1, 1);

        texture.SetPixel(0, 0, color);
        texture.Apply();
        bodyGuiStyle.normal.textColor = isCurrent ? Color.white : descriptionColor;
        bodyGuiStyle.alignment = TextAnchor.MiddleCenter;
        bodyGuiStyle.padding = new RectOffset(textMarginHor, textMarginHor, textMarginVer, textMarginVer);
        bodyGuiStyle.fontStyle = FontStyle.Normal;
        bodyGuiStyle.wordWrap = true;
        bodyGuiStyle.normal.background = texture;
    }
}
