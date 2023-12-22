
namespace DAL
{
    public interface IProvider<T> where T : class
    {
        public List<T> Load();
        public void Save(List<T> listToSave);
    }
    interface IStudy
    {
        public string Study();
    }
    interface ICompose
    {
        public string Compose();
    }
    interface IFly
    {
        public string Fly();
    }
    interface ISkate
    {
        public string Skate();
    }
}
