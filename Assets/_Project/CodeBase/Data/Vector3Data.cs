﻿using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class Vector3Data
    {
        public float X, Y, Z;

        public Vector3Data() {}
        
        public Vector3Data(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}