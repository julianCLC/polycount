using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
// using Shapes2D;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] GameObject GameUIObject; // change this depending on desktop or mobile
    [SerializeField] Animator countdownAnimator;
    public float countdownTime;
    float countdownTimeModifier;
    [SerializeField] Transform PlayArea;
    [SerializeField] GameObject retryScreen;
    [SerializeField] GameObject confirmExitScreen;
    [SerializeField] LevelUtilities levelUtils;

    ShapePooler shapePooler;
    List<ShapeScript> allShapes = new List<ShapeScript>();
    public bool colorChangeAugment = false;
    public bool sizeChangeAugment = false;

    GameMode gameMode = GameMode.Normal; // something should set this, have a listener to the menu

    public static event Action onLevelStart;
    public static event Action onLevelExit;

    public static event Action<int> onNextLevel;
    public static event Action<int> onLevelLose;
    public static event Action onWrongAnswer;

    public static event Action onPauseEnter;
    public static event Action onPauseExit;

    // Level Data
    Shapes correctAnswer;
    int level;
    int[] shapeRatio; // ex. 2:1:1


    float currentTime;
    float timeIncrease;

    // TEMP data used for next level
    public int numShapes = 5;
    int numChoices = 3;

    void Awake(){
        
    }

    void OnEnable(){
        // onNextLevel += NextLevelSequence;

        GameManager.onEnterGame += StartSequence;
        RetryScript.onRetry += StartGame;

        Choice.onChoiceMade += SubmitAnswer;

        LifeManager.onZeroHearts += GameOver;
        LevelTimer.onTimerEnd += GameOver;

        GameBackButton.onBackButton += OnBackButtonDown;
        ExitScreen.onReturnGame += OnReturnGame;
        ExitScreen.onConfirmExit += OnConfirmExit;
        RetryScript.onConfirmExit += OnConfirmExit;
    }

    void OnDisable(){
        // onNextLevel -= NextLevelSequence;

        GameManager.onEnterGame -= StartSequence;
        RetryScript.onRetry -= StartGame;
        
        Choice.onChoiceMade -= SubmitAnswer;

        LifeManager.onZeroHearts -= GameOver;
        LevelTimer.onTimerEnd -= GameOver;
        
        GameBackButton.onBackButton -= OnBackButtonDown;
        ExitScreen.onReturnGame -= OnReturnGame;
        ExitScreen.onConfirmExit -= OnConfirmExit;
        RetryScript.onConfirmExit -= OnConfirmExit;
    }

    // Start is called before the first frame update
    void Start()
    {
        shapePooler = ObjectPoolManager.instance.GetPoolByType<ShapePooler>(PoolType.shape);
        UpdateAnimClipTimes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSequence(){
        //
        GameUIObject.SetActive(true);

        countdownAnimator.SetFloat("countdownSpeed", countdownTimeModifier);
        countdownAnimator.Play("CountdownAnim");
        StartCoroutine(countDown(countdownTime));
    }

    void StartGame(){
        Debug.Log("StartGame");
        // initialize level data
        CleanUpLevel();

        numShapes = 5;
        level = 1;
        LevelSetup(numShapes, numChoices, correctAnswer, 0.4f, true, level);

        onLevelStart?.Invoke();
    }

    void NextLevelSequence(){
        level++;
        CleanUpLevel();
        correctAnswer = LevelUtilities.ChooseRandomAnswer();

        // TEMP
        NewLevelProperties();
        LevelSetup(numShapes, numChoices, correctAnswer, 0.4f, true, level);

        onNextLevel?.Invoke(level);

    }

    void NewLevelProperties(){
        int shapesToAdd = 3;

        if(level == 6){
            numShapes = 2;
            // levelHandler.sizeChangeAugment = true;
        }
        if(level == 11){
            // equalSplit = false; 
            numShapes -= 5;
        }
        if(level == 21){
            // levelHandler.colorChangeAugment = true;
            // levelHandler.sizeChangeAugment = false;
            // equalSplit = true;
            numShapes = 5;
            shapesToAdd = 1;
        }
        if(level == 26){
            numShapes -= 5;
        }
        if(level == 31){
            numShapes -= 10;
            // levelHandler.sizeChangeAugment = true;
        }

        numShapes += shapesToAdd;
    }

    public void LevelSetup(int _numShapes, int _numChoices, Shapes answer, float answerPercent, bool equalSplit, int level){
        // total shapes
        // choose answer
        // choose percent of each shape
        // choose augments (moving, size, etc)

        correctAnswer = answer;

        // ~~ Get amount of each Shape ~~
        int[] ShapeDivision;

        if(!LevelUtilities.NumDivider(_numShapes, _numChoices, answerPercent, equalSplit, out ShapeDivision)){
            Debug.Log("ERROR");
            return;
        }

        // ~~ Place shapes on screen ~~~
        int i = 1;
        int spawnAmount;
        

        foreach(Shapes shape in Enum.GetValues(typeof(Shapes))){
            // Ensure corrent shape is spawned the most
            if(shape == correctAnswer){
                spawnAmount = ShapeDivision[0];
            }
            else {
                spawnAmount = ShapeDivision[i];
                i++;
            }

            // Spawn Shape
            for(int j = 0; j < spawnAmount; j++){
                // choose random point in play area
                Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(-PlayArea.localScale.x/2, PlayArea.localScale.x/2) + PlayArea.position.x,
                                                UnityEngine.Random.Range(-PlayArea.localScale.y/2, PlayArea.localScale.y/2) + PlayArea.position.y);

                // get and place shape
                ShapeScript newShape = GetNewShape(shape);
                allShapes.Add(newShape);
                newShape.transform.position = spawnPos;
            }
            
        }
    }

    ShapeScript GetNewShape(Shapes shape){
        ShapeScript newShape = shapePooler.Get();
        newShape.InitializeShape(shape);

        levelUtils.ApplyAugments(newShape.transform);
        // define augments in LevelUtilites
        // design:
        // > general pre augment etc. change material property
        // > augment each shape before spawning
        // > general post augment 

        // Add augments to shape
        /*
        if(sizeChangeAugment) {
            newShape.AugmentSize();
        }
        if(colorChangeAugment){
            Shapes shapeColorToUse = (Shapes)Enum.ToObject(typeof(Shapes), UnityEngine.Random.Range(0,3));
            newShape.SetColour(ResourceLoader.GetDefaultColour(shapeColorToUse));
        }
        */

        return newShape;
    }

    void CleanUpLevel(){
        foreach(ShapeScript shapeScript in allShapes){
            shapeScript.DestroyShape();
        }
        allShapes.Clear();
    }

    public void SubmitAnswer(Shapes choice){
        if(CheckAnswer(choice)){
            SoundManager.instance.PlaySound("CorrectSFX");
            NextLevelSequence();
        }
        else{
            SoundManager.instance.PlaySound("WrongSFX");
            onWrongAnswer?.Invoke();
        }
    }

    public bool CheckAnswer(Shapes submitted){
        return submitted == correctAnswer ? true : false;
    }

    void GameOver(){
        retryScreen.SetActive(true);
        onLevelLose?.Invoke(level);
    }

    void OnBackButtonDown(){
        Time.timeScale = 0;
        onPauseEnter?.Invoke();
        confirmExitScreen.SetActive(true);
    }

    void OnReturnGame(){
        Time.timeScale = 1;
        onPauseExit?.Invoke();
    }

    void OnConfirmExit(){
        
        GameUIObject.SetActive(false);
        OnReturnGame();
        onLevelExit?.Invoke();

        // Do this last or the shapes won't return to normal
        CleanUpLevel();
    }

    IEnumerator countDown(float timeToWait){
        yield return new WaitForSeconds(timeToWait);
        StartGame();
    }

    public void UpdateAnimClipTimes(){
        AnimationClip[] clips = countdownAnimator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips){
            if(clip.name == "CountdownAnim"){
                countdownTimeModifier = clip.length / countdownTime;
                // countdownAnimator.SetFloat("countdownSpeed", countdownTimeModifier);
            }
        }
    }
}

public enum GameMode{
    Normal,
    Zen
}
