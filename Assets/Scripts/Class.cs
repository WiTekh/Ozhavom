using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

public class Class : MonoBehaviour
{
    private float hpMax;
    private float velocity;
    private float atkSpeed;
    private int[] abilities;
    private Sprite skin;
    
    public Class(float hpMax, float velocity, float atkSpeed, int[] abilities, Sprite skin)
    {
        if (abilities.Length != this.abilities.Length)
            throw new ArgumentException("Class : The number of abilities of this Class is invalid");

        this.hpMax = hpMax;
        this.velocity = velocity;
        this.atkSpeed = atkSpeed;
        this.abilities = abilities;
        this.skin = skin;
    }
}