using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeScript : MonoBehaviour
{
    // script for shape prefab
    public Shapes shape;
    private Action<ShapeScript> _killAction;
    [SerializeField] SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable(){
        LevelHandler.onPauseEnter += OnPauseEnter;
        LevelHandler.onPauseExit += OnPauseExit;
    }

    void OnDisable(){
        LevelHandler.onPauseEnter -= OnPauseEnter;
        LevelHandler.onPauseExit -= OnPauseExit;
    }

    public void InitializeShape(Shapes _shape){
        // set shape and set random position;
        shape = _shape;
        // transform.position = pos;
        // transform.localScale = Vector3.one;
        transform.localScale = new Vector2(1.5f, 1.5f);
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = ResourceLoader.GetSprite(shape);
        // spriteRenderer.color = ResourceLoader.GetHexColour(ResourceLoader.GetDefaultColour(shape));

        // Move random direction
        rb.AddForce(UnityEngine.Random.insideUnitCircle * 2f, ForceMode2D.Impulse);

        

        
    }

    // public void SetColour(string hexCode ){
    //     spriteRenderer.color = ResourceLoader.GetHexColour(hexCode);
    // }

    public void AugmentSize(){
        float newScale = UnityEngine.Random.Range(0.5f, 2f);
        transform.localScale = new Vector3(newScale, newScale, 1);
    }

    public void DestroyShape(){
        // in case sprite renderer is still turned off
        // spriteRenderer.enabled = true;
        _killAction(this);
    }

    public void OnUseSetup(Action<ShapeScript> killAction){
        _killAction = killAction;
    }

    public void OnPauseEnter(){
        spriteRenderer.enabled = false;
    }

    public void OnPauseExit(){
        Debug.Log("EXIT PAUSE");
        spriteRenderer.enabled = true;

    }
}
