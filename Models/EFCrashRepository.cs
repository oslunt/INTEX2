using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models
{
    public class EFCrashRepository : ICrashRepository

    {
        private CrashDbContext _context { get; set; }
        public EFCrashRepository(CrashDbContext temp)
        {
            _context = temp;
        }
        public IQueryable<Crash> Crashes => _context.Crashes;

        IQueryable<Crash> ICrashRepository.Crashes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void SaveCrash(Crash c)
        {
            if (c.CRASH_ID == 0)
            {
                var max = _context.Crashes.Max(x => x.CRASH_ID);
                c.CRASH_ID = max + 1;
                _context.Update(c);
                _context.SaveChanges();
            }

            else
            {
                _context.Update(c);
                _context.SaveChanges();
            }
        }

        public void AddCrash(Crash c)
        {
            if (c.CRASH_ID == 0)
            {
                var max = _context.Crashes.Max(x => x.CRASH_ID);
                c.CRASH_ID = max + 1;
                _context.Add(c);
                _context.SaveChanges();
            }

            else
            {
                _context.Add(c);
                _context.SaveChanges();
            }
        }

        public void DeleteCrash(Crash c)
        {
            _context.Remove(c);
            _context.SaveChanges();
        }
    }
}
