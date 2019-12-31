namespace Application.Models
{
    public class EFInfoRepository : IInfoRepository
    {
        private readonly ApplicationDBContext _context;

        public EFInfoRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Info Add(Info info)
        {
            _context.Infos.Add(info);
            _context.SaveChanges();

            return info;
        }

        public Info GetInfo(int id)
        {
            return _context.Infos.Find(id);
        }

        public Info Update(Info infoUpdate)
        {
            var inf = _context.Infos.Attach(infoUpdate);
            inf.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return infoUpdate;
        }
    }
}
