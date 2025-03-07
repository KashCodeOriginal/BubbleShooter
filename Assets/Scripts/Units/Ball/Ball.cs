﻿using UnityEngine;

public struct Ball
{
    private Color _color;
    private BallTypeBehavior _ballType;

    public Color Color => _color;
    public BallTypeBehavior BallType => _ballType;

    public void Modify(Color color, BallTypeBehavior ballType)
    {
        _color = color;
        _ballType = ballType;
    }
}