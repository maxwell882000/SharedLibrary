using SharedLibrary.Attributes;

namespace SharedLibrary.Helpers
{
    public class Pagination<T> where T : IEntity
    {
        private IQueryable<T> query;
        private int page;
        private int take;
        public Pagination(IQueryable<T> query, int page = 1, int take = 8)
        {
            this.query = query;
            this.page = page;
            this.take = take;
        }


        public (IQueryable<T>, int) paginate()
        {
            int number = this.query.Count();
            int maxPage = (((int)number + take - 1) / take);
            int skip = 0;
            if (this.page >= 1 && maxPage > this.page)
                skip = (this.page - 1) * take;
            else
            {
                skip = ((int)maxPage - 1) * take;
            }

            if (skip < 0)
            {
                skip = 0;
            }
            return (this.query.OrderByDescending(e => e.Id).Skip(skip).Take(take), maxPage);
        }
    }
}