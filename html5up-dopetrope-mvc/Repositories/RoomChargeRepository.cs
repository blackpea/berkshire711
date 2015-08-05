using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc.Repositories
{
    using System.Data.Entity.Repository;

    using html5up_dopetrope_mvc.Models;

    public class RoomChargeRepository : RepositoryBase<BedAndBreakfastEntities>, IRoomChargeRepository
    {
        public RoomChargeRepository()
            : base(throwExceptions: true, useTransactions: true)
        {
            base.RepositoryBaseExceptionRaised += RoomChargeRepository_RepositoryBaseExceptionRaised;
        }

        void RoomChargeRepository_RepositoryBaseExceptionRaised(Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool AddRoomCharge(TB_RoomCharge roomCharge)
        {
            bool result = false;

            result = Add<TB_RoomCharge>(roomCharge);

            return result;
        }

        public bool AddRoomChargeWithoutTransaction(TB_RoomCharge roomCharge)
        {
            SetUseTransaction(false);

            bool result = false;

            result = Add<TB_RoomCharge>(roomCharge);

            return result;
        }

        public bool AddOrUpdateRoomCharge(TB_RoomCharge roomCharge)
        {
            bool result = false;

            result = AddOrUpdate<TB_RoomCharge>(roomCharge);

            return result;
        }

        public bool DeleteRoomCharge(TB_RoomCharge roomCharge)
        {
            bool result = false;

            result = Delete<TB_RoomCharge>(roomCharge);

            return result;
        }

        public bool UpdateRoomCharge(TB_RoomCharge roomCharge)
        {
            bool result = false;

            result = Update<TB_RoomCharge>(roomCharge);

            return result;
        }
    }
}