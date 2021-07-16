namespace Module
{
    public enum Errorcase 
    { 
        [StringValue("Inappropriate ID")] Error1, 
        [StringValue("Inappropriate PWD")] Error2,
        [StringValue("Not matched PWD")] Error3,
        [StringValue("Duplicated ID")] Error4
    }    
}