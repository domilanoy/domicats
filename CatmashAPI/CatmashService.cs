using System.Collections.Generic;
using System.Linq;

namespace CatmashAPI
{
    public class CatmashService
    {
        public List<Cat> Cats
        {
            get
            {
                using (var db = new CatContext())
                {
                    return db.Cats.ToList();
                }
            }
        }
        public int VoteCount
        {
            get
            {
                return Cats.Sum(c => c.Score);
            }
        }
        public bool SetVote(string catId)
        {
            bool ok = false;
            using (var db = new CatContext())
            {
                var result = db.Cats.SingleOrDefault(c => c.Id == catId);
                if (result != null)
                {
                    result.Score += 1;
                    db.SaveChanges();
                    ok = true;
                }
            }
            return ok;
        }
    }
}
