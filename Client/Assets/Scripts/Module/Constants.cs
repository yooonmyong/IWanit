using System;
using System.Collections;
using UnityEngine;

namespace Module
{
    public static class Constants
    {
        public const double FullMotive = 10.0f;
        public const int OneMonths = 30;
        public const int OneDay = 86400;
        public const int SpeedOfElapsedTime = 5000;

        public const int MinimumofAutomaticalUpdate = 500;
        public const int MaximumofAutomaticalUpdate = 100000;
        public const double HandlingDigit = 0.1;

        public const int MinStandardtoRememberWord = 3;
        public const int MaxStandardtoRememberWord = 50;

        public const int MinStandardtoRememberWord = 3;
        public const int MaxStandardtoRememberWord = 50;

        public const int MonthsofPottyTraining = 20;

        public static readonly int[] ProperTemperatures = { 11, 17 };
        public const float ZoomIn = 300.0f;
        public static readonly Vector2 LeftToolTransform = 
            new Vector2(-150, 200);
        public static readonly Vector2 RightToolTransform = 
            new Vector2(150, 200);
        public const int BrushingEnough = 15;
        public const double MinimalCleanliness = 1.0f;

        public static readonly Vector3 InitializedTransform = 
            new Vector3(-270, -250, 1);
        public static readonly Vector3 BrushingTeethTransform = 
            new Vector3(-280, -250, 1);
        
        public const float StartingValueinRow = 0f;
        public const float StartingValueinColumn = 1f;
        public const float RowInterval = 0.5f;
        public const float ColumnInterval = 0.5f;
        public const int MaxElementsinaRow = 3;
    }
}