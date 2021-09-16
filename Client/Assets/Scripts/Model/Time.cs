using System;
using System.Collections;
using System.Collections.Generic;
using Realms;

namespace Model
{
    public class Time : RealmObject
    {
        [PrimaryKey]
        public Guid ID
        {
            get; set;
        }

        public float CurrentTime
        {
            get; set;
        }

        public int ElapsedDays
        {
            get; set;
        }

        public DateTimeOffset BirthDay
        {
            get; set;
        }

        public Time()
        {
        }

        public Time(Guid id)
        {
            this.ID = id;
            this.CurrentTime = 0.0f;
            this.ElapsedDays = 0;
            this.BirthDay = 
                DateTime.SpecifyKind
                (
                    DateTime.Now.AddDays(-7).Date, DateTimeKind.Utc
                );
        }
    }
}