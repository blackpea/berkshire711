using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc.Repositories
{
    using System.Data.Entity.Repository;

    using html5up_dopetrope_mvc.Models;

    public class RoomRepository : RepositoryBase<BedAndBreakfastEntities>, IRoomRepository
    {
        public RoomRepository()
            : base(throwExceptions: true, useTransactions: true)
        {
            base.RepositoryBaseExceptionRaised += RoomRepository_RepositoryBaseExceptionRaised;
        }

        void RoomRepository_RepositoryBaseExceptionRaised(Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool AddRoom(TB_Room room)
        {
            bool result = false;

            result = Add<TB_Room>(room);

            return result;
        }

        public bool AddRoomWithoutTransaction(TB_Room room)
        {
            SetUseTransaction(false);

            bool result = false;

            result = Add<TB_Room>(room);

            return result;
        }

        public bool AddOrUpdateRoom(TB_Room room)
        {
            bool result = false;

            result = AddOrUpdate<TB_Room>(room);

            return result;
        }

        public bool DeleteRoom(TB_Room room)
        {
            bool result = false;

            result = Delete<TB_Room>(room);

            return result;
        }

        public bool UpdateRoom(TB_Room room)
        {
            bool result = false;

            result = Update<TB_Room>(room);

            return result;
        }
    }
}