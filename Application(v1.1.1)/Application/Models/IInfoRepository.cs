namespace Application.Models
{
    public interface IInfoRepository
    {
        Info Add(Info info);
        Info GetInfo(int id);
        Info Update(Info infoUpdate);
    }
}
