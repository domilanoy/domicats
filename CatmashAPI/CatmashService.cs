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
        public int GetScore(string catId)
        {
            int score = -1;
            using (var db = new CatContext())
            {
                var cat = db.Cats.SingleOrDefault(c => c.Id == catId);
                if (cat != null) score = cat.Score;
            }
            return score;
        }
        public bool SetVote(string catId)
        {
            bool ok = false;
            using (var db = new CatContext())
            {
                var cat = db.Cats.SingleOrDefault(c => c.Id == catId);
                if (cat != null)
                {
                    cat.Score += 1;
                    db.SaveChanges();
                    ok = true;
                }
            }
            return ok;
        }
    }
}
