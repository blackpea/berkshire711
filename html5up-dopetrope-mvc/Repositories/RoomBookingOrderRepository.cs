using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc.Repositories
{
    using System.Data.Entity.Repository;

    using html5up_dopetrope_mvc.Models;

    public class RoomBookingOrderRepository : RepositoryBase<BedAndBreakfastEntities>, IRoomBookingOrderRepository
    {
        public RoomBookingOrderRepository()
            : base(throwExceptions: true, useTransactions: true)
        {
            base.RepositoryBaseExceptionRaised += RoomBookingOrderRepository_RepositoryBaseExceptionRaised;
        }

        void RoomBookingOrderRepository_RepositoryBaseExceptionRaised(Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool AddRoomBookingOrder(TB_RoomBookingOrder roomBookingOrder)
        {
            bool result = false;

            result = Add<TB_RoomBookingOrder>(roomBookingOrder);

            return result;
        }

        public bool AddRoomBookingOrderWithoutTransaction(TB_RoomBookingOrder roomBookingOrder)
        {
            SetUseTransaction(false);

            bool result = false;

            result = Add<TB_RoomBookingOrder>(roomBookingOrder);

            return result;
        }

        public bool AddOrUpdateRoomBookingOrder(TB_RoomBookingOrder roomBookingOrder)
        {
            bool result = false;

            result = AddOrUpdate<TB_RoomBookingOrder>(roomBookingOrder);

            return result;
        }

        public bool DeleteRoomBookingOrder(TB_RoomBookingOrder roomBookingOrder)
        {
            bool result = false;

            result = Delete<TB_RoomBookingOrder>(roomBookingOrder);

            return result;
        }

        public bool UpdateRoomBookingOrder(TB_RoomBookingOrder roomBookingOrder)
        {
            bool result = false;

            result = Update<TB_RoomBookingOrder>(roomBookingOrder);

            return result;
        }
    }
}