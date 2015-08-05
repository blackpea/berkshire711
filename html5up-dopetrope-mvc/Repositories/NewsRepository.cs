using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc.Repositories
{
    using System.Data.Entity.Repository;

    using html5up_dopetrope_mvc.Models;

    public class NewsRepository : RepositoryBase<BedAndBreakfastEntities>, INewsRepository
    {
        public NewsRepository()
            : base(throwExceptions: true, useTransactions: true)
        {
            base.RepositoryBaseExceptionRaised += NewsRepository_RepositoryBaseExceptionRaised;
        }

        void NewsRepository_RepositoryBaseExceptionRaised(Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool AddNews(TB_News news)
        {
            bool result = false;

            result = Add<TB_News>(news);

            return result;
        }

        public bool AddNewsWithoutTransaction(TB_News news)
        {
            SetUseTransaction(false);

            bool result = false;

            result = Add<TB_News>(news);

            return result;
        }

        public bool AddOrUpdateNews(TB_News news)
        {
            bool result = false;

            result = AddOrUpdate<TB_News>(news);

            return result;
        }

        public bool DeleteNews(TB_News news)
        {
            bool result = false;

            result = Delete<TB_News>(news);

            return result;
        }

        public bool UpdateNews(TB_News news)
        {
            bool result = false;

            result = Update<TB_News>(news);

            return result;
        }
    }
}