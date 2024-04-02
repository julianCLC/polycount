using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelUtilities : MonoBehaviour
{
    // Size Change
    public bool sizeChangeAugment;
    public bool setSpecificScale;
    public float targetScale;

    // Apply Movement
    public bool applyForceAugment;
    public float forceToApply;

    // Static methods
    public static bool NumDivider(int totalNum, int totalChoices, float primaryPercent, bool equalSplit, out int[] result){
        /// Splits amount(totalNum) into different amount buckets(totalChoices)
        /// bucket on index 0 gets the most amount, specified by primaryPercent
        /// rest of the buckets gets distributed evenly : MODIFY THIS EVENTUALLY FOR MORE CONTROL

        // primaryPercent = what % the mode shape has to be
        // mostShapesIndex = which shape to increase the percent of (index of answer)
        // int[] result = primary shape is in index 0
        
        int numPrimaryShape, numSecondaryShape, numTertiaryShape;

        result = new int[totalChoices];
        if(primaryPercent <= 0.3){ return false; }

        numPrimaryShape = (int)(totalNum * primaryPercent);

        if(equalSplit){
            float secondaryPercent = (1 - primaryPercent)/2f;
            numSecondaryShape = (int)(totalNum * secondaryPercent);
            numTertiaryShape = numSecondaryShape;
        }
        else{
            float secondaryPercent = 1 - (primaryPercent + 0.2f);
            numSecondaryShape = (int)(totalNum * secondaryPercent);
            numTertiaryShape = (int)(1 - (primaryPercent + secondaryPercent));
        }
        int remainder = totalNum - (numPrimaryShape + numSecondaryShape + numTertiaryShape);

        result[0] = numPrimaryShape;
        result[1] = numSecondaryShape;
        result[2] = numTertiaryShape;
        /*
        for(int i = 1; i < totalChoices; i++){
            result[i] = numSecondaryShape;
        }
        */

        // Distribute remainder to all the divisions
        int j = 0;
        while(remainder > 0){
            if((j != 0) && (result[j] + 1) >= result[0]){
                j = (j + 1) % 3;
            }
            else{
                result[j]++;
                j = (j + 1) % 3;
                remainder--;
            }
            

            
        }

        return true;

    }
    public void ApplyAugments(Transform shape){

        if(sizeChangeAugment){
            AugmentSize(shape);
        }

        if(applyForceAugment){
            AugmentAddMovement(shape);
        }
    }

    public static Shapes ChooseRandomAnswer(){
        Shapes[] shapeList = Enum.GetValues(typeof(Shapes)) as Shapes[];
        Shapes shapeToReturn = shapeList[UnityEngine.Random.Range(0,3)];
        return shapeToReturn;
    }

    // AUGMENT SETTERS
    public void InitAugmentSize(float _targetScale, bool setScale, bool setSizeChange){
        targetScale = _targetScale;
        setSpecificScale = setScale;
        sizeChangeAugment = setSizeChange;
    }

    public void InitAugmentAddMovement(float _targetForce, bool setMovementChange){
        forceToApply = _targetForce;
        applyForceAugment = setMovementChange;
    }

    // SHAPE AUGMENT FUNCTIONS
    void AugmentSize(Transform shape){
        if(setSpecificScale){
            shape.localScale = new Vector3(targetScale, targetScale, 1);
        }
        else{
            float newScale = UnityEngine.Random.Range(0.5f, 2f);
            shape.localScale = new Vector3(newScale, newScale, 1);
        }
    }



    void AugmentAddMovement(Transform shape){
        shape.GetComponent<Rigidbody2D>().AddForce(UnityEngine.Random.insideUnitCircle * forceToApply, ForceMode2D.Impulse);
    }
}
