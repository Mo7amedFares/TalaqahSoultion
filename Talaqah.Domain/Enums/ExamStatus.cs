namespace Talaqah.Domain.Enums;

public enum ExamStatus
{
    Draft =1, //Admin is preparing
    Published =2, //Students can take it
    Closed =3, //No new attempts
    Archived =4  //Old exam but available for reports
}
