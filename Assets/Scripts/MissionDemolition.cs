using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    public static MissionDemolition S;

    public GameObject[] castles;
    public Text levelText;
    public Text shotsText;
    public Vector3 castlePosition;

    public bool _____________________________;

    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject currentCastle;
    public GameMode mode = GameMode.idle;
    public string showing = "Slingshot";

    void Start()
    {
        S = this;

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if (currentCastle != null)
        {
            Destroy(currentCastle);
        }

        GameObject[] oldProjectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (var projectile in oldProjectiles)
        {
            Destroy(projectile);
        }

        currentCastle = Instantiate(castles[level]);
        currentCastle.transform.position = castlePosition;
        shotsTaken = 0;

        SwitchView("Both");
        ProjectileLine.S.Clear();
        Goal.goalMet = false;

        UpdateGameText();

        mode = GameMode.playing;
    }

    void UpdateGameText()
    {
        levelText.text = "Level: " + (level + 1) + " of " + levelMax;
        shotsText.text = "Shots: " + shotsTaken;
    }

    void Update()
    {
        UpdateGameText();

        if (mode == GameMode.playing && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Both");
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    void OnGUI()
    {
        Rect buttonRect = new Rect((Screen.width / 2) - 50, 10, 100, 24);

        switch (showing)
        {
            case "Slingshot":
                if (GUI.Button(buttonRect, "Show Castle"))
                {
                    SwitchView("Castle");
                }
                break;

            case "Castle":
                if (GUI.Button(buttonRect, "Show Both"))
                {
                    SwitchView("Both");
                }
                break;

            case "Both":
                if (GUI.Button(buttonRect, "Show Slingshot"))
                {
                    SwitchView("Slingshot");
                }
                break;
        }
    }

    public static void SwitchView(string view)
    {
        S.showing = view;
        switch (view)
        {
            case "Slingshot":
                FollowCam.S.pointOfInterest = null;
                break;

            case "Castle":
                FollowCam.S.pointOfInterest = S.currentCastle;
                break;

            case "Both":
                FollowCam.S.pointOfInterest = GameObject.Find("ViewBoth");
                break;
        }
    }

    public static void ShotFired()
    {
        S.shotsTaken++;
    }

    public static void RemoveShotFired()
    {
        S.shotsTaken--;
    }
}
