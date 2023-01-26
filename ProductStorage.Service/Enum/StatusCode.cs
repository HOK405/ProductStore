using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.Service.Enum
{
    public enum StatusCode
    {
        EntityIsNull = 0,
        // User doesn't exist in the collection
        // Success
        OK = 200,
        // 0 items in the collection
        ZeroItemsFound = 300,
        // InternalServerError
        InternalServerError = 500,

        UserNotFound = 600,

        ProductNotFound = 700,

        CustomerProductNotFound = 800
    }
}
