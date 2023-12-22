using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;



namespace DAL
{
    [JsonDerivedType(typeof(Entity), typeDiscriminator: "base")]
    [JsonDerivedType(typeof(Student), typeDiscriminator: "student")]
    [JsonDerivedType(typeof(Pilot), typeDiscriminator: "Pilot")]
    [JsonDerivedType(typeof(Musician), typeDiscriminator: "Musician")]
    [Serializable]
    [XmlInclude(typeof(Student))]
    [XmlInclude(typeof(Pilot))]
    [XmlInclude(typeof(Musician))]
    public class Entity :  ISkate
    {
        [XmlElement]
        public string LastName { get; set; }
        public Entity() { }
        [JsonConstructor]
        public Entity(string LastNameInput)
        {
            LastName = LastNameInput;
        }
        public virtual string[] Methods { get { return new string[] { "Skating" }; } }
        public string Skate()
        {
            return LastName + " is skating.";
        }
        public override string ToString() => LastName;
        protected Entity(SerializationInfo info, StreamingContext context)
        {
            LastName = info.GetString("LastName");
           
        }
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LastName", LastName);
            
        }
    }
    [Serializable]
    public class Student : Entity, IStudy
    {
        private string studentID;
        private string gender;
        private int? course;
        private int? dormNumber;
        private int? dormRoom;
        public string StudentID { get => studentID; set => studentID = value; }
        //public int? GPA { get => gpa; set => gpa = value; }
        public string? Gender { get => gender; set => gender = value; }
        public int? Course { get => course; set => course = value; }
        public int? DormNumber { get => dormNumber; set => dormNumber = value; }
        public int? DormRoom { get => dormRoom; set => dormRoom = value; }
        public Student() { }
        protected Student(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            StudentID = info.GetString("StudentID");
            Gender = info.GetString("Gender");
            Course = info.GetInt32("Course");
            DormNumber = info.GetInt32("DormNumber");
            DormRoom = info.GetInt32("DormRoom");
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("StudentID", StudentID);
            info.AddValue("Course", Course);
            info.AddValue("Gender", Gender);
            info.AddValue("DormNumber", DormNumber);
            info.AddValue("DormRoom", DormRoom);
        }
        public Student(string LastNameInput, string StudentIDInput) : base(LastNameInput)
        {
            studentID = StudentIDInput;
        }
        [JsonConstructor]
        public Student(int? Course, string StudentID, string Gender, int? DormNumber, int? DormRoom, string LastName) : base(LastName)
        {
            (studentID, gender, course, dormNumber, dormRoom) = (StudentID, Gender, Course, DormNumber, DormRoom);
        }
        public Student(string LastName, string StudentID, int? Course, string Gender, int? DormNumber, int? DormRoom) :
            this(Course, StudentID, Gender, DormNumber, DormRoom, LastName)
        { }
        public string Study()
        {
            course = course == 6 ? 1 : course + 1;
            return LastName + " is now studing in " + course + " course";
        }

        public override string[] Methods { get { return base.Methods.Union(new string[] { "Study" }).ToArray(); } }
        public override string ToString() =>
            "Student - " + LastName +
            ", StudentID: " + studentID +
            ", Course: " + Course +
            ", Gender: " + Gender +
            ", DormNember: " + DormNumber +
            ", DormRoom: " + DormRoom;
    }
    [Serializable]
    public class Pilot : Entity, IFly
    {
        public Pilot() { }
        public Pilot(string LastName) : base(LastName) { }
        public string Fly()
        {
            return LastName + " is flying";
        }
        public override string[] Methods { get { return base.Methods.Union(new string[] { "Fly" }).ToArray(); } }
        public override string ToString() => "Pilot - " + LastName;
        protected Pilot(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
    [Serializable]
    public class Musician : Entity, ICompose
    {
        public Musician() { }
        public Musician(string LastName) : base(LastName) { }
        public string Compose()
        {
            return LastName + " is composing.";
        }
        public override string[] Methods { get { return base.Methods.Union(new string[] { "Compose" }).ToArray(); } }
        public override string ToString() => "Musician - " + LastName;
        protected Musician(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
