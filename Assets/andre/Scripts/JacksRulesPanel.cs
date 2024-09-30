using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class JacksRulesPanel : MonoBehaviour
{
    private Text rulesText;

    void Start()
    {
        // Create a new UI panel
        GameObject panel = new GameObject("Jacks Rules Panel");
        panel.transform.SetParent(transform);

        // Add a RectTransform component to the panel
        RectTransform rectTransform = panel.AddComponent<RectTransform>();
        rectTransform.anchorMin = new UnityEngine.Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new UnityEngine.Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = new UnityEngine.Vector2(400, 300);

        // Add an Image component to the panel for a background
        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

        // Add a Text component to display the rules
        rulesText = panel.AddComponent<Text>();
        //        rulesText.text = """
        //Here's a summary of the basic rules for the game of jacks in bullet point format:

        //Equipment: A small rubber ball and a set of metal jacks (usually 10)
        //Players: Typically 2-4 players, but can be played solo
        //Gameplay:Scatter the jacks on a flat surface
        //Toss the ball up, pick up jacks, then catch the ball after one bounce
        //Start with "onesies" (picking up one jack at a time)
        //Progress through "twosies", "threesies", etc., picking up more jacks each round
        //If successful, move to the next number; if not, the turn passes to the next player
        //Rounds:Players must complete each number(1 - 10) to finish a round
        //New rounds may introduce variations like "no bounce" or "double bounce"
        //Winning: The first player to complete all rounds wins
        //Alternatively, play for a set number of rounds and count total jacks collected
        //Fouls:Failing to pick up the correct number of jacks
        //Not catching the ball after one bounce
        //Moving or touching other jacks while picking up the target jacks
        //Dropping picked-up jacks before catching the ball
        //""";

        rulesText.text = $@"
Here's a summary of the basic rules for the game of Jacks:

• Equipment: A small rubber ball and a set of metal jacks (usually 10)

• Players: Typically 2-4 players, but can be played solo

• Gameplay:
  - Scatter the jacks on a flat surface
  - Toss the ball up, pick up jacks, then catch the ball after one bounce
  - Start with ""onesies"" (picking up one jack at a time)
  - Progress through ""twosies"", ""threesies"", etc., picking up more jacks each round
  - If successful, move to the next number; if not, the turn passes to the next player

• Rounds:
  - Players must complete each number (1-10) to finish a round
  - New rounds may introduce variations like ""no bounce"" or ""double bounce""

• Winning:
  - The first player to complete all rounds wins
  - Alternatively, play for a set number of rounds and count total jacks collected

• Fouls:
  - Failing to pick up the correct number of jacks
  - Not catching the ball after one bounce
  - Moving or touching other jacks while picking up the target jacks
  - Dropping picked-up jacks before catching the ball
";

        rulesText.rectTransform.sizeDelta = new UnityEngine.Vector2(380, 280);
        rulesText.alignment = TextAnchor.MiddleCenter;
        rulesText.fontSize = 18;
        rulesText.color = Color.white;
    }
}