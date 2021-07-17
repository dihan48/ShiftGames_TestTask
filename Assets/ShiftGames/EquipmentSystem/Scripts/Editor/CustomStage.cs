using UnityEngine;
using UnityEditor.SceneManagement;

public class CustomStage : PreviewSceneStage
{
    protected override GUIContent CreateHeaderContent() => new GUIContent(name);
}